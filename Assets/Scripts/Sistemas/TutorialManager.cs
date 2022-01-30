using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public Sprite[] scenes;

    [TextArea(3, 10)]
    public string[] descriptions;
    public string[] tittle;
    public Button forwardButton, backButton;

    [SerializeField] private TextMeshProUGUI tittleDisplay;
    [SerializeField] private Image imageDisplay;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private string nextScene;

    private int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        tittleDisplay.text = tittle[0];
        imageDisplay.sprite = scenes[0];
        textDisplay.text = descriptions[0];
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

        tittleDisplay.text = tittle[currentIndex];
        imageDisplay.sprite = scenes[currentIndex];
        textDisplay.text = descriptions[currentIndex];
    }
}
