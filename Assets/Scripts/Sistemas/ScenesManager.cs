using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void GoToScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        Debug.Log("A");
    }
}
