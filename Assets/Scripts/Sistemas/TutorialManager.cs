using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class TutorialManager : MonoBehaviour
{
    public Sprite[] scenes;

    [TextArea(3, 10)]
    public string[] descriptions;
    public string[] titles;
    public Button forwardButton, backButton;

    [SerializeField] private TextMeshProUGUI titleDisplay;
    [SerializeField] private Image imageDisplay;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private string nextScene;

    private int currentIndex;
    
    public Sprite[] scenesEN;

    [TextArea(3, 10)]
    public string[] descriptionsEN;
    public string[] titlesEN;
    private Sprite[] _scenes;
    private string[] _descriptions;
    private string[] _titles;

    void UpdateLocale()
    {
        var selectedLocale = LocalizationSettings.SelectedLocale;
        switch (selectedLocale.Identifier.Code)
        {
            case "en":
                _scenes = scenesEN;
                _descriptions = descriptionsEN;
                _titles = titlesEN;

                break;
            default:
            case "pt":
                _scenes = scenes;
                _descriptions = descriptions;
                _titles = titles;
                break;
        }
    }

    void Start()
    {
        titleDisplay.text = titles[0];
        imageDisplay.sprite = scenes[0];
        imageDisplay.preserveAspect = true;
        textDisplay.text = descriptions[0];
        currentIndex = 0;
        Navigate(0);
    }

    public void Navigate(int indexStep)
    {
        UpdateLocale();
        if (indexStep < 0 && currentIndex == 0)
        {
            return;
        }

        if (indexStep > 0 && currentIndex == scenes.Length - 1)
        {
            return;
        }
        currentIndex += indexStep;

        backButton.gameObject.SetActive(currentIndex != 0);
        forwardButton.gameObject.SetActive(currentIndex != scenes.Length - 1);

        titleDisplay.text = _titles[currentIndex];
        imageDisplay.sprite = _scenes[currentIndex];
        imageDisplay.preserveAspect = true;
        textDisplay.text = _descriptions[currentIndex];
    }
}
