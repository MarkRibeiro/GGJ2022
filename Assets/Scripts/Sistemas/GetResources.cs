using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetResources : MonoBehaviour
{
    public DeckManager dm;
    public BattleSystem bs;
    public Image p_EmotionImage, p_ReasonImage, e_EmotionImage, e_ReasonImage;
    public TextMeshProUGUI p_EmotionText, p_ReasonText, e_EmotionText, e_ReasonText;
    public Sprite p_EmotionSprite, p_ReasonSprite, y_EmotionSprite, y_ReasonSprite;

    // Start is called before the first frame update
    void Start()
    {
        p_EmotionText.text = dm.player.emotion.ToString() + "/" + bs.resource_limit.ToString();
        p_ReasonText.text = dm.player.reason.ToString() + "/" + bs.resource_limit.ToString();
        e_EmotionText.text = dm.enemy.emotion.ToString() + "/" + bs.resource_limit.ToString();
        e_ReasonText.text = dm.enemy.reason.ToString() + "/" + bs.resource_limit.ToString();

        if(CharacterManager.playerID == 0)
        {
            p_EmotionImage.sprite = y_EmotionSprite;
            p_ReasonImage.sprite = y_ReasonSprite;

            e_EmotionImage.sprite = p_EmotionSprite;
            e_ReasonImage.sprite = p_ReasonSprite;
        }
        else
        {
            p_EmotionImage.sprite = p_EmotionSprite;
            p_ReasonImage.sprite = p_ReasonSprite;

            e_EmotionImage.sprite = y_EmotionSprite;
            e_ReasonImage.sprite = y_ReasonSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        p_EmotionText.text = dm.player.emotion.ToString() + "/" + bs.resource_limit.ToString();
        p_ReasonText.text = dm.player.reason.ToString() + "/" + bs.resource_limit.ToString();
        e_EmotionText.text = dm.enemy.emotion.ToString() + "/" + bs.resource_limit.ToString();
        e_ReasonText.text = dm.enemy.reason.ToString() + "/" + bs.resource_limit.ToString();
    }
}
