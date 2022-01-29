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
    public BattleState state;
    public DeckManager dm;

    [SerializeField] private int reason;
    [SerializeField] private int emotion;

    [SerializeField] private int resource_limit;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        BeginBattle();
    }

    private void BeginBattle()
    {
        state = BattleState.PLAYER_TURN;
        PlayerTurn();
    }

    private void PlayerTurn()
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
        dm.DiscardHand();
        dm.Shuffle();
        dm.DrawHand();
    }

    private int RollDice()
    {
        int result = Random.Range(1, 7);
        return result;
    }
}
