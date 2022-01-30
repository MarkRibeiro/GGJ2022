using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetResources : MonoBehaviour
{
    public DeckManager dm;
    public BattleSystem bs;
    public TextMeshProUGUI p_EmotionText, p_ReasonText, e_EmotionText, e_ReasonText;

    // Start is called before the first frame update
    void Start()
    {
        p_EmotionText.text = dm.player.emotion.ToString() + "/" + bs.resource_limit.ToString();
        p_ReasonText.text = dm.player.reason.ToString() + "/" + bs.resource_limit.ToString();
        e_EmotionText.text = dm.enemy.emotion.ToString() + "/" + bs.resource_limit.ToString();
        e_ReasonText.text = dm.enemy.reason.ToString() + "/" + bs.resource_limit.ToString();
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
