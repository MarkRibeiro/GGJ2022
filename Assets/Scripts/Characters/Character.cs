using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character: MonoBehaviour
{
    public enum Emotions {
        NORMAL,
        SHAME,
        SMUG
    }

    //Recursos
    public int reason;
    public int emotion;

    //Cartas
    public Card[] deck;
    public List<GameObject> currentHand;

    //Barras
    public int maxHP;
    public int currHP;
    public int maxShield;
    public int currShield;

    public Slider healthBar;
    public Slider shieldBar;
}
