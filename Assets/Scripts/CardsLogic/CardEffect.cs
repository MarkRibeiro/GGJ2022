using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    CHANGE_EXPRESSION,
    SHAME,
    ATTACK,
    DEFENSE
}

public enum Expressions
{
    SHAME,
    SMUG,
    NORMAL,
    KEEP,
    RANDOM
}

[System.Serializable]
public class CardEffect
{
    public int effectValue;
    public bool AffectHp;
    public CardType cardType;
    public Expressions changeTo;
    public Expressions RightExpression;

}