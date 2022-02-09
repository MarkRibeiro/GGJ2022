using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

public class CutsceneManager : MonoBehaviour
{
    public Sprite[] scenes;

    [TextArea(3, 10)]
    public string[] descriptions;
    public TextMeshProUGUI buttonText;

    [SerializeField] private Image imageDisplay;
    [SerializeField] private TextMeshProUGUI textDiaplay;
    [SerializeField] private string nextScene;

    private int currentIndex;

    [SerializeField]
    GameObject backArrow, nextArrow;

    [TextArea(3, 10)]
    public string[] descriptionsEN;

    private string[] _descriptions;
    private string _skipText;
    private string _nextText;

    void UpdateLocale()
    {
        var selectedLocale = LocalizationSettings.SelectedLocale;
        _skipText = LocalizationSettings.StringDatabase.GetLocalizedString("Pular");
        _nextText = LocalizationSettings.StringDatabase.GetLocalizedString("Proximo");
        switch (selectedLocale.Identifier.Code)
        {
            case "en":
               _descriptions = descriptionsEN;
                break;
            default:
            case "pt":
                _descriptions = descriptions;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateLocale();
        imageDisplay.sprite = scenes[0];
        textDiaplay.text = _descriptions[0];
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
            SceneManager.LoadScene(nextScene);
            return;
        }

        currentIndex += indexStep;

        if (backArrow != null && nextArrow != null)
        {
            backArrow.SetActive(currentIndex != 0);
            nextArrow.SetActive(currentIndex != scenes.Length - 1);
        }

        buttonText.text = currentIndex == scenes.Length - 1 ? _nextText : _skipText;

        imageDisplay.sprite = scenes[currentIndex];
        textDiaplay.text = _descriptions[currentIndex];
    }
}
