using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {
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
        dm.DrawHand(currentChar.deck, currentChar.currentHand);
    }

    private void PlayerTurn()
    {
        StartTurn(dm.player);
    }

    private void EnemyTurn()
    {
        StartTurn(dm.enemy);

        //Jogar cartas da sua mao, se possivel
        foreach(GameObject card in dm.enemy.currentHand)
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

        for(int i = 0; i < 2; i++)
        {
            int result = Random.Range(1, 7);
            Debug.Log(result);
            switch(result)
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

        if(currentChar.reason + reason_gain > resource_limit)
            currentChar.reason = resource_limit;
        else
            currentChar.reason += reason_gain;

        if(currentChar.emotion + emotion_gain > resource_limit)
            currentChar.emotion = resource_limit;
        else
            currentChar.emotion += emotion_gain;
    }

    public void PlayCard(CardInstance playedCard, Character currentChar)
    {
        //Subtrair custos da carta
        if(playedCard.card.brainsCost > currentChar.reason || playedCard.card.heartCost > currentChar.emotion)
        {
            return;
        }

        currentChar.reason -= playedCard.card.brainsCost;
        currentChar.emotion -= playedCard.card.heartCost;

        //Aplicar efeito
        ApplyEffect(playedCard.card.effect);

        Destroy(playedCard.gameObject);
    }

    public void ApplyEffect(CardEffect effect)
    {
        if(effect.cardType == CardType.ATTACK)
        {
            //checar se e no miolo ou casca
            //aplicar dano
        }
        else if(effect.cardType == CardType.DEFENSE)
        {
            //checar se e no miolo ou casca
            //aplicar cura/escudo
        }
        else if(effect.cardType == CardType.SHAME)
        {
            //verificar se a expression atual e a correta
        }
        else if(effect.cardType == CardType.CHANGE_EXPRESSION)
        {
            //mudar expression do oponente
        }
    }

    public void EndTurn()
    {
        if(state == BattleState.PLAYER_TURN)
        {
            state = BattleState.ENEMY_TURN;
            EnemyTurn();
        }
        else if(state == BattleState.ENEMY_TURN)
        {
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }
    }
}
