using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetResources : MonoBehaviour
{
    public DeckManager dm;
    public TextMeshProUGUI p_EmotionText, p_ReasonText, e_EmotionText, e_ReasonText;

    // Start is called before the first frame update
    void Start()
    {
        p_EmotionText.text = dm.player.emotion.ToString();
        p_ReasonText.text = dm.player.reason.ToString();
        e_EmotionText.text = dm.enemy.emotion.ToString();
        e_ReasonText.text = dm.enemy.reason.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        p_EmotionText.text = dm.player.emotion.ToString();
        p_ReasonText.text = dm.player.reason.ToString();
        e_EmotionText.text = dm.enemy.emotion.ToString();
        e_ReasonText.text = dm.enemy.reason.ToString();
    }
}
