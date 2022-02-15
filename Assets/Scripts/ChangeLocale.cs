using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLocale : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public void SetLocale(int i)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }

    private void OnEnable()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        if (dropdown != null)
        {
            dropdown.value = LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        }
        else
        {
            Debug.LogWarning("No dropdown found");
        }
    }
}
