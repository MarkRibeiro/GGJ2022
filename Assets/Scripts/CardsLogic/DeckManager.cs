using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{

    public Card[] deck;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
