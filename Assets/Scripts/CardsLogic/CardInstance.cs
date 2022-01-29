using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInstance : MonoBehaviour
{
    public Card card;
    public Text cardName;
    public Text description;
    public Text b_costText;
    public Text h_costText;
    public Text effectText;
    public Image cardImage;


    // Start is called before the first frame update
    void Start()
    {
        cardName.text = card.cardName;
        description.text = card.description;
        b_costText.text = card.brainsCost.ToString();
        h_costText.text = card.brainsCost.ToString();
        effectText.text = card.effect.ToString();

        cardImage.sprite = card.sprite;
    }
}
