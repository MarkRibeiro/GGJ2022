using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLocale : MonoBehaviour
{
    public void SetLocale(int i)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }
}
