using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
    ATTACK,
    DEFENSE
}

[System.Serializable]
public class CardEffect
{
    public int effectValue;
    public bool ignoreShield;
    public CardType cardType;
}