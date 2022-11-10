using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameObject mainmenuImage;
    public GameObject optionsImage;
    public GameObject inGameImage;
    public GameObject pauseImage;
    public GameObject pauseackgroundImage;
    public GameObject inventoryImage;
    public GameObject loseScreenImage;
    public GameObject winScreenImage;

    [SerializeField] AudioSource buttonClick;

    public void ButtonsSoundPlay()
    {
        buttonClick.volume = PlayerPrefs.GetFloat("volume");
        buttonClick.Play();
    }
    public void PlayButton()
    {
        mainmenuImage.SetActive(false);
        inGameImage.SetActive(true);
    }

    public void OptionsButton()
    {
        optionsImage.SetActive(true);
        pauseImage.SetActive(false);
        
    }

    public void ExitOptionsbutton()
    {
        optionsImage.SetActive(false);
        pauseImage.SetActive(true);
        pauseackgroundImage.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseButton ()
    {
        pauseImage.SetActive(true);
        pauseackgroundImage.SetActive(true);
        Time.timeScale = 0;
        // + stop the timer, freeze enemies/towers/vfx
    }

    public void ResumeButton()
    {
        //pauseImage.SetActive(false);
        //pauseackgroundImage.SetActive(false);
        Time.timeScale = PlayerPrefs.GetFloat("speed");
        // + resume the timer, unfreeze enemies/towers/vfx
    }

    public void InventoryButton()
    {
        inventoryImage.SetActive(true);
    }


    public void CloseInventory()
    {
        inventoryImage.SetActive(false);
    }

    public void NextWaveButton()
    {
        //next wave
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgainButton()
    {
        winScreenImage.SetActive(false);
        loseScreenImage.SetActive(false);
        //inGameImage.SetActive(true);
        ////reset the level
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ResetEnemyList()
    {
        Enemy.enemyList = new List<Enemy>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
