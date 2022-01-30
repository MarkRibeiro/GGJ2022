using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public BattleState state;
    public DeckManager dm;

    [SerializeField] private int resource_limit;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        state = BattleState.START;
        BeginBattle();
    }

    private void BeginBattle()
    {
        state = BattleState.PLAYER_TURN;
        PlayerTurn();
    }

    private void StartTurn(Character currentChar)
    {
        //Rolar dados
        RollDice(currentChar);

        //Comprar carta
        dm.DiscardHand(currentChar.currentHand);
        dm.Shuffle(currentChar.deck);
        dm.DrawHand(currentChar.deck, currentChar.currentHand, currentChar.handArea);
    }

    private void PlayerTurn()
    {
        StartTurn(dm.player);
    }

    private void EnemyTurn()
    {
        StartTurn(dm.enemy);

        //Jogar cartas da sua mao, se possivel
        foreach (GameObject card in dm.enemy.currentHand)
        {
            CardInstance instance = card.GetComponent<CardInstance>();
            PlayCard(instance, dm.enemy);
        }

        EndTurn();
    }

    private void RollDice(Character currentChar)
    {
        int reason_gain = 0;
        int emotion_gain = 0;

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

        //Aplicar efeito
        ApplyEffect(playedCard.card.effect, currentChar);

        Destroy(playedCard.gameObject);
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
                        character.target.currShield -= effect.effectValue;
                        character.target.shieldBar.SetValue(character.target.currShield);
                    }
                }
                character.target.ChangeExpression(effect.changeTo);
                break;
            case CardType.DEFENSE:
                if (effect.AffectHp)
                {
                    character.currHP += effect.effectValue;
                    character.healthBar.SetValue(character.currHP);
                }
                else
                {
                    character.currShield += effect.effectValue;
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

    public void EndTurn()
    {
        if (state == BattleState.PLAYER_TURN)
        {
            state = BattleState.ENEMY_TURN;
            EnemyTurn();
        }
        else if (state == BattleState.ENEMY_TURN)
        {
            Debug.Log("Fim do turno do oponente");
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }
    }

    public void EndMatch(Character winner)
    {
        if (winner == dm.player)
        {
            //Tela de vitoria
        }
        else
        {
            //Tela de derrota
        }
    }

}
