using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Character player, enemy;
    [SerializeField] private int handSize;
    [SerializeField] private GameObject cardPrefab;

    public void Shuffle(List<Card> deck)
    {
        for(int i = 0; i < deck.Count - 1; i++)
        {
            int index = Random.Range(i, deck.Count);
            Card randomCard = deck[index];
            deck[index] = deck[i];
            deck[i] = randomCard;
        }
    }

    public void DrawHand(List<Card> deck, List<GameObject> currentHand, RectTransform handArea)
    {
        CardInstance newCard = cardPrefab.GetComponent<CardInstance>();

        for(int i = 0; i < handSize; i++)
        {
            newCard.card = deck[i];
            GameObject instance = Instantiate(cardPrefab, handArea.transform);
            currentHand.Add(instance);
        }
    }

    public void DiscardHand(List<GameObject> currentHand)
    {
        foreach(GameObject card in currentHand)
        {
            Destroy(card);
        }
        currentHand.Clear();
    }
}
