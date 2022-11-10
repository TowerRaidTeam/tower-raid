using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialImages;
    [SerializeField] private GameObject tutorialPanel;
    private int index = 0;

    private void Start()
    {
        //if (PlayerPrefs.GetString("tutorial") == "done")
        //{
        //    Destroy(this);
        //}
        //else
        //{
        //    tutorialPanel.SetActive(true);
        //    tutorialImages[index].SetActive(true);
        //}
    }

   public void NextImage()
    {
        index++;
        if (index >= tutorialImages.Length)
        {
            PlayerPrefs.SetString("tutorial", "done");
            this.enabled = false;
        }
        for (int i = 0; i < tutorialImages.Length; i++)
        {
            if (i != index)
            {
                tutorialImages[i].SetActive(false);
            }
            else
            {
                tutorialImages[i].SetActive(true);
            }
        }
        
    }

}
