using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("PlayableOne");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
