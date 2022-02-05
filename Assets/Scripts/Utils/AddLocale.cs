using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Collections;
using TMPro;
using UnityEngine.Localization.Components;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010

#if (UNITY_EDITOR)
public class AddLocale : ScriptableWizard
{
    public GameObject[] objects;


    [MenuItem("Custom/Add LocalizeStringEvent")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Add LocalizeStringEvent", typeof(AddLocale), "Add");
    }

    void OnWizardCreate()
    {
        foreach (GameObject go in objects)
        {
            AddLocalizedStringEvent(go.transform);
        }
    }


    void AddLocalizedStringEvent(Transform t)
    {
        TextMeshProUGUI text = t.GetComponent<TextMeshProUGUI>();
        if(text != null)
        {
            var lse = t.GetComponent<LocalizeStringEvent>();
            if(lse == null)
            {
                lse = t.gameObject.AddComponent<LocalizeStringEvent>();
            }
        }
        foreach (Transform child in t)
        {
            AddLocalizedStringEvent(child);
        }
    }
}
#endif

