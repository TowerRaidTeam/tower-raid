using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Slider audioSlider;
    [SerializeField] AudioSource[] mainMenuAudio;

    private void Start()
    {
        audioSlider.value = PlayerPrefs.GetFloat("volume");
        AudioChangeMenu();
    }
    //public void StartGame()
    //{
    //    PlayerPrefs.DeleteAll();
    //    SceneManager.LoadScene("PlayableOne");
    //}

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AudioChangeMenu()
    {
        foreach (AudioSource item in mainMenuAudio)
        {
            item.volume = audioSlider.value;
        }
        
        PlayerPrefs.SetFloat("volume", audioSlider.value);
    }

}
