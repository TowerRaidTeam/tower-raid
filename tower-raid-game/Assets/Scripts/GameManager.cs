using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D cursourTexture;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] TMP_Text cashText;
    [SerializeField] Slider gameSpeedSlider;
    int cash = 0;

    [SerializeField] Button spawnEnemysButtons;
    [SerializeField] TMP_Text waveText;

    [SerializeField] Image healtBarImage;
    float hp = 100;
    SortingArray sortingArray;
    [SerializeField] private GameObject enemyWizard;
    [SerializeField] private Vector3 enemySpawnPosition;

    public static bool isExtendable = false;
    public static bool spawnEnemies = false;

    int waveIndex = 0; 

    private void Start()
    {
        Cursor.SetCursor(cursourTexture, Vector2.zero, CursorMode.Auto);

        gameSpeedSlider.value = 1;
        Time.timeScale = gameSpeedSlider.value;

        UpdateWaveCounter(0);
        AddCash(0);
        sortingArray = FindObjectOfType<SortingArray>();
    }

    public static bool  GetTurretHitInfo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static GameObject GetTurretHitGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            if (hit.transform.gameObject.tag == "Tower")
            {
                return hit.transform.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void StartSpawningEnemys()
    {
        WorldGeneration.path = sortingArray.GenerateNewPath().ToArray();
        enemySpawnPosition = WorldGeneration.path[WorldGeneration.path.Length - 1] + Vector3.up;
        UpdateWaveCounter(1);
        StartCoroutine(SpawnEnemy());
    }

    public void StopSpawningEnemys()
    {
        StopCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        spawnEnemysButtons.interactable = false;
        spawnEnemies = true;
        for (int i = 0; i < 10; i++)
        {
            Instantiate(enemyWizard, enemySpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        spawnEnemies = false;
        spawnEnemysButtons.interactable = true;
    }

    public void TakeDmgCastle(float dmg)
    {
        hp -= dmg * 100;
        healtBarImage.fillAmount -= dmg;

        if (hp <= 0)
        {
            loseScreen.SetActive(true);
        }
    }

    void UpdateWaveCounter(int index)
    {
        waveIndex += index;
        if (waveIndex > 10)
        {
            Debug.Log("YOU WIN");
        }
        else
        {
            waveText.text ="WAWE" + "\n" + waveIndex + "/10";
        }
       
    }

    public void AddCash(int moneyAmount)
    {
        cash += moneyAmount;
        cashText.text = cash + "$";
    }

    public void UpdatedGameSpeedSlider()
    {
        Time.timeScale = gameSpeedSlider.value;
    }

}
