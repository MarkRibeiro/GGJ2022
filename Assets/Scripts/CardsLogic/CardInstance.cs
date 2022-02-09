using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;

public class CardInstance : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI b_costText;
    public TextMeshProUGUI h_costText;
    public Image cardImage;
    public Image bgImage;
    public Image typeImage;
    public Image effectImage;
    public Image border;

    public Image Overlay;


    private DeckManager dm;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DeckManager>();
        float margin = gameObject.transform.parent.GetComponent<HandArea>().GetMargin();

        UpdateLocale();

        b_costText.text = card.brainsCost.ToString();
        h_costText.text = card.heartCost.ToString();

        if (CharacterManager.playerID == 0)
        {
            cardImage.sprite = card.y_mainSprite;
            if (margin == 100)
            {
                cardImage.sprite = card.p_mainSprite;
            }
        }
        else
        {
            cardImage.sprite = card.p_mainSprite;
            if (margin == 100)
            {
                cardImage.sprite = card.y_mainSprite;
            }
        }

        typeImage.sprite = card.typeSprite;
        effectImage.sprite = card.effectSprite;

        border.color = card.color;
    }

    public void UpdateLocale()
    {
        var selectedLocale = LocalizationSettings.SelectedLocale;
        switch (selectedLocale.Identifier.Code)
        {
            case "en":
                cardName.text = card.cardNameEN;
                description.text = card.descriptionEN;
                break;
            default:
            case "pt":
                cardName.text = card.cardName;
                description.text = card.description;
                break;
        }
    }

    public void Play()
    {
        DeckManager dm = FindObjectOfType<DeckManager>();
        BattleSystem.instance.PlayCard(gameObject.GetComponent<CardInstance>(), dm.player);
        var instance = AudioManager.instance;
        if (instance != null)
        {
            instance.Play("CardPlay");
        }
    }
}
