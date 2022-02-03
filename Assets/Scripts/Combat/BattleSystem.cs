using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public Color InvalidField = Color.grey;
    public Color InvalidCard = Color.red;
    public BattleState state;
    public bool FinalMatch = false;
    public DeckManager dm;
    public int resource_limit;
    public GameObject endMatchScreen;

    [SerializeField] private Sprite[] y_diceSides, p_diceSides;
    [SerializeField] private GameObject[] dices;

    [SerializeField] private string victoryText, defeatText;
    [SerializeField] private Sprite y_endSprite, p_endSprite, goodEnding;
    [SerializeField] private float diceTime = 1.0f;
    [SerializeField] private float enemyTime = 1.0f;

    [SerializeField] private GameObject enemyCardArea;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private GameObject turnTextBox;
    [SerializeField] private GameObject endTurnButton;


    private Color playerColor, enemyColor;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        state = BattleState.START;

        if(CharacterManager.playerID == 0)
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
        turnText.text = "Seu turno";
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

    }

    private void PlayerTurn()
    {
        StartCoroutine(StartTurn(dm.player));
    }

    private IEnumerator EnemyTurn()
    {
        yield return StartCoroutine(StartTurn(dm.enemy));

        //Jogar cartas da sua mao, se possivel
        foreach (GameObject card in dm.enemy.currentHand)
        {
            CardInstance instance = card.GetComponent<CardInstance>();
            PlayCard(instance, dm.enemy);
            yield return new WaitForSeconds(enemyTime);
            card.transform.position = dm.enemy.handArea.transform.position;
        }

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

    public void PlayCard(CardInstance playedCard, Character currentChar)
    {
        //Subtrair custos da carta
        if (playedCard.card.brainsCost > currentChar.reason || playedCard.card.heartCost > currentChar.emotion)
        {
            return;
        }

        currentChar.reason -= playedCard.card.brainsCost;
        currentChar.emotion -= playedCard.card.heartCost;

        if (currentChar == dm.enemy)
        {
            playedCard.gameObject.transform.position = enemyCardArea.transform.position;
        }

        //Aplicar efeito
        ApplyEffect(playedCard.card.effect, currentChar);

        //Destroy(playedCard.gameObject);
        VerifyCards(currentChar);
    }

    public void ApplyEffect(CardEffect effect, Character character)
    {
        switch (effect.cardType)
        {
            case CardType.ATTACK:
                if (effect.AffectHp)
                {
                    if (character.target.currShield <= 0)
                    {
                        character.target.currHP -= effect.effectValue;
                        character.target.healthBar.SetValue(character.target.currHP);
                        if (character.target.currHP <= 0)
                        {
                            EndMatch(character);
                        }
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
                }
                character.target.ChangeExpression(effect.changeTo);
                break;
            case CardType.DEFENSE:
                if (effect.AffectHp)
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
                if (character.target.currentExpression == effect.RightExpression)
                {
                    character.target.currHP -= effect.effectValue;
                    character.target.ChangeExpression(effect.changeTo);
                    character.target.healthBar.SetValue(character.target.currHP);
                    if (character.target.currHP <= 0)
                    {
                        EndMatch(character);
                    }
                }
                else
                {
                    character.currHP -= effect.effectValue;
                    character.ChangeExpression(effect.changeTo);
                    character.healthBar.SetValue(character.currHP);
                    if (character.currHP <= 0)
                    {
                        EndMatch(character.target);
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
            turnText.text = "Turno do oponente";
            StartCoroutine(EnemyTurn());
        }
        else if (state == BattleState.ENEMY_TURN)
        {
            state = BattleState.PLAYER_TURN;
            endTurnButton.SetActive(true);
            turnTextBox.GetComponent<Image>().color = playerColor;
            turnText.text = "Seu turno";
            PlayerTurn();
        }
    }

    public void EndMatch(Character winner)
    {
        endMatchScreen.SetActive(true);
        dm.player.handArea.gameObject.SetActive(false);

        if (winner == dm.player)
        {
            CharacterManager.PlayerPoints++;
            endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = victoryText;

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
            endMatchScreen.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = defeatText;
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
                _instance.bgImage.color = InvalidCard;
                card.GetComponent<CardDrag>().enabled = false;
                if (_instance.card.brainsCost > character.reason)
                {
                    _instance.b_costText.color = InvalidField;
                }
                if (_instance.card.heartCost > character.emotion)
                {
                    _instance.h_costText.color = InvalidField;
                }

            }

        }
    }

}
