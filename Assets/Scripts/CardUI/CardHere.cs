using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHere : MonoBehaviour
{

    public GameObject jogueAqui;
    public int numeroDeVezesDaAjuda;
    [HideInInspector]
    public static int cartasJogadas = 0;

    // Update is called once per frame
    void Update()
    {
        BattleSystem battleSystem = BattleSystem.instance;
        var dm = battleSystem.dm;
        var cards = dm.player.currentHand;

        if (cards != null)
        {
            foreach (var card in cards)
            {
                if (card != null && cartasJogadas < numeroDeVezesDaAjuda)
                {
                    CardHover cardHover = card.GetComponent<CardHover>();
                    CardInstance cardInstance = card.GetComponent<CardInstance>();
                    if (cardHover.isHovered && cardInstance.card.brainsCost <= dm.player.reason && cardInstance.card.heartCost <= dm.player.emotion)
                    {
                        jogueAqui.SetActive(true);
                        return;
                    }
                }
            }
            jogueAqui.SetActive(false);
        }
    }
}
