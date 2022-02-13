using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;

public enum BattleState
{
    START,
    PLAYER_TURN,
    ENEMY_TURN,
    WON,
    LOST
}
public class BattleSystem : MonoBehaviour
{
    public static BattleSystem instance;
    public Color InvalidField;

    public BattleState state;
    public bool FinalMatch = false;
    public DeckManager dm;
    public int resource_limit;
    public float cardOverlay;
    public GameObject endMatchScreen;

    [SerializeField] private Sprite[] y_diceSides, p_diceSides;
    [SerializeField] private GameObject[] dices;

    [SerializeField] private string victoryText, defeatText;
    [SerializeField] LocalizedString victoryText_LS, defeatText_LS;
    [SerializeField] private Sprite y_endSprite, p_endSprite, goodEnding;
    [SerializeField] private float diceTime = 1.0f;
    [SerializeField] private float enemyTime = 1.0f;
    [SerializeField] private float enemyCardTime = 1.5f;

    [SerializeField] [Range(0, 1)] private float wrongShameChance = 0.3f;

    [SerializeField] private GameObject enemyCardArea;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private GameObject turnTextBox;
    [SerializeField] private GameObject endTurnButton;
    [SerializeField] private GameObject blockSprite;



    private Color playerColor, enemyColor;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        state = BattleState.START;

        if (CharacterManager.playerID == 0)
        {
            ColorUtility.TryParseHtmlString("#BCA0F0", out enemyColor);
            ColorUtility.TryParseHtmlString("#F1CF87", out playerColor);
        }
        else
        {
            ColorUtility.TryParseHtmlString("#BCA0F0", out playerColor);
            ColorUtility.TryParseHtmlString("#F1CF87", out enemyColor);
        }

        BeginBattle();
    }

    private void BeginBattle()
    {

        state = BattleState.PLAYER_TURN;
        turnTextBox.GetComponent<Image>().color = playerColor;
        turnText.text = LocalizationSettings.StringDatabase.GetLocalizedString("Seu Turno");
        PlayerTurn();

    }

    private IEnumerator StartTurn(Character currentChar)
    {
        //Rolar dados
        yield return StartCoroutine(RollDice(currentChar));

        yield return new WaitForSeconds(diceTime);

        dices[0].SetActive(false);
        dices[1].SetActive(false);

        //Comprar carta
        dm.Shuffle(currentChar.deck.cards);
        dm.DrawHand(currentChar.deck.cards, currentChar.currentHand, currentChar.handArea);
        VerifyCards(currentChar);

        if (currentChar == dm.player)
        {
            endTurnButton.SetActive(true);
        }
    }

    private void PlayerTurn()
    {
        StartCoroutine(StartTurn(dm.player));
    }

    private GameObject ChooseCard(List<GameObject> allPossibleCards)
    {
        var enemy = dm.enemy;
        var player = dm.player;
        List<GameObject> eligibleCards = new List<GameObject>();

        foreach (GameObject cardGO in allPossibleCards)
        {
            Card card = cardGO.GetComponent<CardInstance>().card;
            if (card.effect.cardType == CardType.ATTACK)
            {
                if (card.effect.affectHp)
                {
                    Debug.Log(card.name);
                }
            }

            if (enemy.currShield >= enemy.maxShield && card.effect.cardType == CardType.DEFENSE && !card.effect.affectHp)
            {
                continue;
            }
            if (enemy.currHP >= enemy.maxHP && card.effect.cardType == CardType.DEFENSE && card.effect.affectHp)
            {
                continue;
            }
            if (player.currShield > 0 && (card.effect.cardType == CardType.ATTACK) && card.effect.affectHp)
            {
                continue;
            }
            if (player.currShield <= 0 && (card.effect.cardType == CardType.ATTACK) && !card.effect.affectHp)
            {
                continue;
            }
            if (card.effect.cardType == CardType.SHAME && (player.currentExpression != card.effect.rightExpression))
            {
                if (Random.Range(0.0f, 1.0f) > wrongShameChance)
                {
                    continue;
                }
            }
            eligibleCards.Add(cardGO);
        }
        if (eligibleCards.Count == 0)
        {
            return null;
        }
        return eligibleCards[Random.Range(0, eligibleCards.Count)];
    }

    private IEnumerator EnemyTurn()
    {

        yield return StartCoroutine(StartTurn(dm.enemy));

        var currentCard = ChooseCard(dm.enemy.currentHand);

        while (currentCard != null)
        {
            dm.enemy.currentHand.Remove(currentCard);
            if (PlayCard(currentCard.GetComponent<CardInstance>(), dm.enemy))
            {
                yield return new WaitForSeconds(enemyTime);
            }
            currentCard = ChooseCard(dm.enemy.currentHand);
        }

        yield return new WaitForSeconds(enemyCardTime);
        EndTurn(dm.enemy);
    }

    private IEnumerator RollDice(Character currentChar)
    {
        int reason_gain = 0;
        int emotion_gain = 0;

        dices[0].SetActive(true);
        dices[1].SetActive(true);

        AudioManager.PlaySound("RolarDado");
        for (int j = 0; j <= 20; j++)
        {
            int randomSide1 = Random.Range(0, 6);
            int randomSide2 = Random.Range(0, 6);

            if (currentChar == dm.player)
            {
                if (CharacterManager.playerID == 0)
                {
                    dices[0].GetComponent<Image>().sprite = y_diceSides[randomSide1];
                    dices[1].GetComponent<Image>().sprite = y_diceSides[randomSide2];
                }
                else
                {
                    dices[0].GetComponent<Image>().sprite = p_diceSides[randomSide1];
                    dices[1].GetComponent<Image>().sprite = p_diceSides[randomSide2];
                }
            }
            else
            {
                if (CharacterManager.playerID == 0)
                {
                    dices[0].GetComponent<Image>().sprite = p_diceSides[randomSide1];
                    dices[1].GetComponent<Image>().sprite = p_diceSides[randomSide2];
                }
                else
                {
                    dices[0].GetComponent<Image>().sprite = y_diceSides[randomSide1];
                    dices[1].GetComponent<Image>().sprite = y_diceSides[randomSide2];
                }
            }

            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 2; i++)
        {

            int result = Random.Range(1, 7);

            switch (result)
            {
                case 1:
                    emotion_gain += 1;
                    break;
                case 2:
                    reason_gain += 1;
                    break;
                case 3:
                    emotion_gain += 2;
                    break;
                case 4:
                    reason_gain += 2;
                    break;
                case 5:
                case 6:
                    reason_gain += 1;
                    emotion_gain += 1;
                    break;
            }

            if (currentChar == dm.player)
            {
                if (CharacterManager.playerID == 0)
                {
                    dices[i].GetComponent<Image>().sprite = y_diceSides[result - 1];
                }
                else
                {
                    dices[i].GetComponent<Image>().sprite = p_diceSides[result - 1];
                }
            }
            else
            {
                if (CharacterManager.playerID == 0)
                {
                    dices[i].GetComponent<Image>().sprite = p_diceSides[result - 1];
                }
                else
                {
                    dices[i].GetComponent<Image>().sprite = y_diceSides[result - 1];
                }
            }

        }

        if (currentChar.reason + reason_gain > resource_limit)
            currentChar.reason = resource_limit;
        else
            currentChar.reason += reason_gain;

        if (currentChar.emotion + emotion_gain > resource_limit)
            currentChar.emotion = resource_limit;
        else
            currentChar.emotion += emotion_gain;
    }

    private IEnumerator WaitThenUse(CardInstance card)
    {
        yield return new WaitForSeconds(enemyCardTime);
        card.GetComponent<Animator>().SetTrigger("Use");
        card.GetComponent<Animator>().speed = 0.75f;
    }

    public bool PlayCard(CardInstance playedCard, Character currentChar)
    {
        //Subtrair custos da carta
        if (playedCard.card.brainsCost > currentChar.reason || playedCard.card.heartCost > currentChar.emotion)
        {
            return false;
        }

        currentChar.reason -= playedCard.card.brainsCost;
        currentChar.emotion -= playedCard.card.heartCost;

        if (currentChar == dm.enemy)
        {
            playedCard.transform.SetParent(enemyCardArea.transform);
            playedCard.transform.localPosition = new Vector3(playedCard.transform.localPosition.x, playedCard.transform.localPosition.y, 0);
            //playedCard.transform.localPosition = Vector3.zero;
            var rect = playedCard.GetComponent<RectTransform>();
            rect.offsetMax = new Vector2(rect.offsetMax.x, 0);
            rect.offsetMin = new Vector2(rect.offsetMin.x, 0);
            playedCard.GetComponent<Canvas>().overrideSorting = false;
            StartCoroutine(WaitThenUse(playedCard));
        }

        else
        {
            CardHere.cartasJogadas++;
        }

        //Aplicar efeito
        ApplyEffect(playedCard.card.effect, currentChar);

        // CARTA É DESTRUIDA POR EVENTO DE ANIMAÇÃO

        VerifyCards(currentChar);
        return true;
    }

    public void ApplyEffect(CardEffect effect, Character character)
    {
        switch (effect.cardType)
        {
            case CardType.ATTACK:
                if (effect.affectHp)
                {
                    if (character.target.currShield <= 0)
                    {
                        character.target.currHP -= effect.effectValue;
                        character.target.healthBar.SetValue(character.target.currHP);
                        if (character.target.currHP <= 0)
                        {
                            StartCoroutine(EndMatch(character));
                        }
                    }
                    else
                    {
                        blockSprite.GetComponent<Animator>().SetTrigger("CantPlay");
                    }
                }
                else
                {
                    if (character.target.currShield > 0)
                    {
                        if (character.target.currShield - effect.effectValue < 0)
                        {
                            character.target.currShield = 0;
                        }
                        else
                        {
                            character.target.currShield -= effect.effectValue;
                        }
                        character.target.shieldBar.SetValue(character.target.currShield);
                    }
                    else
                    {
                        blockSprite.GetComponent<Animator>().SetTrigger("CantPlay");
                    }
                }
                character.target.ChangeExpression(effect.changeTo);
                break;
            case CardType.DEFENSE:
                if (effect.affectHp)
                {
                    AudioManager.PlaySound("Lifeup");
                    if (character.currHP + effect.effectValue > character.maxHP)
                    {
                        character.currHP = character.maxHP;
                    }
                    else
                    {
                        character.currHP += effect.effectValue;
                    }
                    character.healthBar.SetValue(character.currHP);
                }
                else
                {
                    AudioManager.PlaySound("GainShield");
                    if (character.currShield + effect.effectValue > character.maxShield)
                    {
                        character.currShield = character.maxShield;
                    }
                    else
                    {
                        character.currShield += effect.effectValue;
                    }

                    character.shieldBar.SetValue(character.currShield);
                }
                if (effect.changeTo != Expressions.KEEP)
                {
                    character.target.ChangeExpression(effect.changeTo);
                }
                break;
            case CardType.SHAME:
                //verificar se a expression atual e a correta
                if (character.target.currentExpression == effect.rightExpression)
                {
                    character.target.currHP -= effect.effectValue;
                    character.target.ChangeExpression(effect.changeTo);
                    character.target.healthBar.SetValue(character.target.currHP);
                    if (character.target.currHP <= 0)
                    {
                        StartCoroutine(EndMatch(character));
                    }
                }
                else
                {
                    character.currHP -= effect.effectValue;
                    character.ChangeExpression(effect.changeTo);
                    character.healthBar.SetValue(character.currHP);
                    if (character.currHP <= 0)
                    {
                        StartCoroutine(EndMatch(character.target));
                    }
                }
                break;
            case CardType.CHANGE_EXPRESSION:
                character.target.ChangeExpression(effect.changeTo);
                break;
        }
    }

    public void EndTurn(Character currentChar)
    {
        dm.DiscardHand(currentChar.currentHand);
        if (state == BattleState.PLAYER_TURN)
        {
            state = BattleState.ENEMY_TURN;
            endTurnButton.SetActive(false);
            turnTextBox.GetComponent<Image>().color = enemyColor;
            turnText.text = LocalizationSettings.StringDatabase.GetLocalizedString("Turno do Oponente");
            StartCoroutine(EnemyTurn());
        }
        else if (state == BattleState.ENEMY_TURN)
        {
            state = BattleState.PLAYER_TURN;
            turnTextBox.GetComponent<Image>().color = playerColor;
            turnText.text = LocalizationSettings.StringDatabase.GetLocalizedString("Seu Turno");
            PlayerTurn();
        }
    }

    public IEnumerator EndMatch(Character winner)
    {
        var loser = winner == dm.player ? dm.enemy : dm.player;
        loser.ChangeExpression(Expressions.DEFEAT);
        yield return new WaitForSeconds(1f);
        endMatchScreen.SetActive(true);
        dm.player.handArea.gameObject.SetActive(false);

        if (winner == dm.player)
        {
            CharacterManager.PlayerPoints++;
            if (victoryText_LS == null)
            {
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = victoryText;

            }
            else
            {
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = victoryText_LS.GetLocalizedString();
            }

            {
                if (CharacterManager.playerID == 0)
                {
                    endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = p_endSprite;
                }
                else
                {

                    endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = y_endSprite;
                }
            }


        }
        else
        {
            if (defeatText_LS == null)
            {
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = defeatText;
            }
            else
            {
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = defeatText_LS.GetLocalizedString();
            }

            if (CharacterManager.playerID == 0)
            {
                endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = y_endSprite;
            }
            else
            {
                endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = p_endSprite;
            }


        }
        if (FinalMatch)
        {
            if (PlayerVictory())
            {
                endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = goodEnding;
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = victoryText;
            }
            else
            {
                if (CharacterManager.playerID == 0)
                {
                    endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = y_endSprite;
                }
                else
                {
                    endMatchScreen.transform.GetChild(0).GetComponent<Image>().sprite = p_endSprite;
                }
                endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = defeatText;

            }
        }

    }

    public bool PlayerVictory()
    {
        if (CharacterManager.PlayerPoints >= 2)
        {
            return true;
        }
        return false;
    }
    public void VerifyCards(Character character)
    {

        foreach (GameObject card in character.currentHand)
        {
            if (card == null) continue;
            CardInstance _instance = card.GetComponent<CardInstance>();

            if (_instance.card.brainsCost > character.reason || _instance.card.heartCost > character.emotion)
            {
                Color tempColor = _instance.Overlay.color;
                tempColor.a = cardOverlay;
                _instance.Overlay.color = tempColor;
                card.GetComponent<CardDrag>().enabled = false;
            }
            else
            {
                Color tempColor = _instance.Overlay.color;
                tempColor.a = 0;
                _instance.Overlay.color = tempColor;
            }

        }
    }

}
