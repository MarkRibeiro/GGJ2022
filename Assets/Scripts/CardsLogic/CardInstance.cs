using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardInstance : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI b_costText;
    public TextMeshProUGUI h_costText;
    public Image cardImage;
    public Image typeImage;
    public Image effectImage;
    public Image border;

    // Start is called before the first frame update
    void Start()
    {
        cardName.text = card.cardName;
        description.text = card.description;
        b_costText.text = card.brainsCost.ToString();
        h_costText.text = card.heartCost.ToString();

        cardImage.sprite = card.mainSprite;
        typeImage.sprite = card.typeSprite;
        effectImage.sprite = card.effectSprite;

        border.color = card.color;
    }

    public void Play()
    {
        DeckManager dm = FindObjectOfType<DeckManager>();
        BattleSystem.instance.PlayCard(gameObject.GetComponent<CardInstance>(), dm.player);
    }
}
