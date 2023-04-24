using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public void PauseButton ()
    {
        pauseImage.SetActive(true);
        pauseackgroundImage.SetActive(true);
        // + stop the timer, freeze enemies/towers/vfx
    }

    public void ResumeButton()
    {
        pauseImage.SetActive(false);
        pauseackgroundImage.SetActive(false);
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
        mainmenuImage.SetActive(true);
        //turn off current image
        inGameImage.SetActive(false);
        pauseImage.SetActive(false);
        loseScreenImage.SetActive(false);
        winScreenImage.SetActive(false);
    }

    public void PlayAgainButton()
    {
        winScreenImage.SetActive(false);
        loseScreenImage.SetActive(false);
        inGameImage.SetActive(true);
        //reset the level
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
