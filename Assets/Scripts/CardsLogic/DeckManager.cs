using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Card[] deck;

    [SerializeField] private int handSize;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject handArea;
    public List<GameObject> currentHand;

    public void Shuffle()
    {
        for(int i = 0; i < deck.Length - 1; i++)
        {
            int index = Random.Range(i, deck.Length);
            Card randomCard = deck[index];
            deck[index] = deck[i];
            deck[i] = randomCard;
        }
    }

    public void DrawHand()
    {
        CardInstance newCard = cardPrefab.GetComponent<CardInstance>();

        for(int i = 0; i < handSize; i++)
        {
            newCard.card = deck[i];
            GameObject instance = Instantiate(cardPrefab, handArea.transform.position, Quaternion.identity);
            currentHand.Add(instance);
        }
    }

    public void DiscardHand()
    {
        foreach(GameObject card in currentHand)
        {
            Destroy(card);
        }
        currentHand.Clear();
    }
}
