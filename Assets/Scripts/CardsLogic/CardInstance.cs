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
    public TextMeshProUGUI effectText;
    public Image cardImage;

    // Start is called before the first frame update
    void Start()
    {
        cardName.text = card.cardName;
        description.text = card.description;
        b_costText.text = card.brainsCost.ToString();
        h_costText.text = card.brainsCost.ToString();
        
        effectText.text = card.effect.effectValue.ToString();

        cardImage.sprite = card.sprite;
    }

    public void Play()
    {
        BattleSystem.instance.PlayCard(gameObject.GetComponent<CardInstance>());
        Destroy(gameObject);
    }
}
