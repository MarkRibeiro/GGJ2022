using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck", menuName = "GGJ2022/Deck", order = 1)]
public class Deck : ScriptableObject
{
    public List<Card> cards;
}
