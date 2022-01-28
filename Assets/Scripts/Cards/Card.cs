using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "GGJ2022/Card", order = 0)]
public class Card : ScriptableObject 
{
    public string cardName;
    public string description;
    public int brainsCost;
    public int hearthCost;
    public int effect;

    public Sprite sprite;
}