using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInstance : MonoBehaviour
{
    [SerializeField] private Card card;

    public Text cardName;
    public Text b_costText;
    public Text h_costText;
    public Text effectText;
    public Image cardImage;

    // Start is called before the first frame update
    void Start()
    {
        cardName.text = card.cardName;
        b_costText.text = card.brainsCost.ToString();
        h_costText.text = card.brainsCost.ToString();
        effectText.text = card.effect.ToString();

        cardImage.sprite = card.sprite;
    }
}
