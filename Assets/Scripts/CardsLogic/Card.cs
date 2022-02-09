using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "Card", menuName = "GGJ2022/Card", order = 0)]
public class Card : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string description;
    public int brainsCost;
    public int heartCost;
    [SerializeField]
    public CardEffect effect;
    public Sprite y_mainSprite;
    public Sprite p_mainSprite;

    public Sprite typeSprite;
    public Sprite effectSprite;
    public Color color;

    [TextArea]
    public string descriptionEN;
    public string cardNameEN;
}
