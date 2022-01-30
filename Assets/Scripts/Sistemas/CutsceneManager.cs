using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public Sprite[] scenes;

    [TextArea(3, 10)]
    public string[] descriptions;
    public Button forwardButton, backButton;

    [SerializeField] private Image imageDisplay;
    [SerializeField] private TextMeshProUGUI textDiaplay;
    [SerializeField] private string nextScene;

    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        imageDisplay.sprite = scenes[0];
        textDiaplay.text = descriptions[0];
        currentIndex = 0;
    }

    public void Navigate(int indexStep)
    {
        if(indexStep < 0 && currentIndex == 0)
        {
            return;
        }

        if(indexStep > 0 && currentIndex == scenes.Length - 1)
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        currentIndex += indexStep;

        imageDisplay.sprite = scenes[currentIndex];
        textDiaplay.text = descriptions[currentIndex];
    }
}
