using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCharacterManager : MonoBehaviour
{
    public Character player;
    public Character enemy;

    //Isso precisa ocorrer antes do Start do battle system
    private void Awake()
    {
        if (CharacterManager.playerID == enemy.ID)
        {

            List<Sprite> temp = player.spriteSheet;
            player.spriteSheet = enemy.spriteSheet;
            enemy.spriteSheet = temp;

            Card[] tempDeck = player.deck;
            player.deck = enemy.deck;
            enemy.deck = tempDeck;

            player.transform.rotation = Quaternion.Euler(0, 180, 0);

            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            // FindObjectOfType<DeckManager>().player = enemy;
            // FindObjectOfType<DeckManager>().enemy = player;

        }
    }

}
