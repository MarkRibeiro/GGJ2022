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
        imageDisplay.preserveAspect = true;
        textDisplay.text = descriptions[0];
        currentIndex = 0;
        Navigate(0);
    }

    public void Navigate(int indexStep)
    {
        if(indexStep < 0 && currentIndex == 0)
        {

            return;
        }

        if(indexStep > 0 && currentIndex == scenes.Length - 1)
        {

            return;
        }
        currentIndex += indexStep;

        backButton.gameObject.SetActive(currentIndex != 0);
        forwardButton.gameObject.SetActive(currentIndex != scenes.Length - 1);

        tittleDisplay.text = tittle[currentIndex];
        imageDisplay.sprite = scenes[currentIndex];
        imageDisplay.preserveAspect = true;
        textDisplay.text = descriptions[currentIndex];
    }
}
