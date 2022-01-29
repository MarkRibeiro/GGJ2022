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

    [SerializeField] private int reason;
    [SerializeField] private int emotion;

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

    private void StartTurn(Card[] deck, List<GameObject> currentHand)
    {
        //Rolar dados
        int reason_gain = RollDice();
        int emotion_gain = RollDice();

        //Adicionar valores de razao e emocao
        if(reason + reason_gain > resource_limit)
        {
            reason = resource_limit;
        }
        else
        {
            reason += reason_gain;
        }

        if(emotion + emotion_gain > resource_limit)
        {
            emotion = resource_limit;
        }
        else
        {
            emotion += emotion_gain;
        }

        //Comprar carta
        dm.DiscardHand(currentHand);
        dm.Shuffle(deck);
        dm.DrawHand(deck, currentHand);
    }

    private void PlayerTurn()
    {
        StartTurn(dm.playerDeck, dm.playerCurrentHand);
    }

    private void EnemyTurn()
    {
        StartTurn(dm.enemyDeck, dm.enemyCurrentHand);

        //Jogar cartas da sua mao, se possivel
        foreach(GameObject card in dm.enemyCurrentHand)
        {
            Card instance = card.GetComponent<CardInstance>().card;
            PlayCard(instance);
        }

        EndTurn();
    }

    private int RollDice()
    {
        int result = Random.Range(1, 7);
        return result;
    }

    public void PlayCard(Card playedCard)
    {
        //Subtrair custos da carta
        if(playedCard.brainsCost > reason || playedCard.heartCost > emotion)
        {
            return;
        }

        reason -= playedCard.brainsCost;
        emotion -= playedCard.heartCost;

        //Aplicar efeito
        Debug.Log(playedCard.effect.effectValue);
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
