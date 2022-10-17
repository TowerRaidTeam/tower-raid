using System.Collections;
using System;
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
    public int cash = 0;

    [SerializeField] Button spawnEnemysButtons;
    [SerializeField] TMP_Text waveText;

    [SerializeField] Image healtBarImage;
    float hp = 100;
    SortingArray sortingArray;
    [Header("----------------FOR ENEMY PREFABS----------------")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Vector3 enemySpawnPosition;

    public static bool isExtendable = false;
    public static bool spawnEnemies = false;

    int waveIndex = 0;
    int numberOfEnemiesToSpawn = 20;

    [Header("FOR SHOP")]
    public static int crystalIndexDeleated = 69; //69 broj koji sigurno nije
    [SerializeField] GameObject parentForSpawning;
    int[] filledInventorySlots = { 0, 0, 0 };
    [SerializeField] GameObject[] elementalCrystals;
    [SerializeField] GameObject[] itemSlots;
    List<GameObject> spawnedCrystals = new List<GameObject>();
    

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
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], enemySpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(1);
        }
        numberOfEnemiesToSpawn += 5;
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
        waveText.text = "WAVE: " + waveIndex;
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

    public void BuyNewCrystal()
    {
        if (cash >= 50)
        {
            cash -= 50;
            UpdateCash();
            foreach (var item in filledInventorySlots)
            {
                if (item == 0)
                {
                    SpawnCrystalOnSlot(item);
                    break;
                }
                else
                {
                    Debug.Log("Inventory Full");
                }
            }
        }
        else
        {
            Debug.Log("not enaugh money");
        }
        
    }

    private void SpawnCrystalOnSlot(float valueChange)
    {
        valueChange = 1;
        //foreach (GameObject slots in itemSlots)
        //{
        //    if (!slots.GetComponent<ItemSlot>().slotFilled)
        //    {
        //        GameObject crystal = Instantiate(elementalCrystals[Random.Range(0, elementalCrystals.Length)], slots.GetComponent<ItemSlot>().ceneterLocation, Quaternion.identity);
        //        crystal.transform.SetParent(parentForSpawning.transform);
        //        slots.GetComponent<ItemSlot>().slotFilled = true;
        //        break;
        //    }
        //}
        for (int i = 0; i < filledInventorySlots.Length; i++)
        {
            if (filledInventorySlots[i] == 0)
            {
                GameObject crystal = Instantiate(elementalCrystals[UnityEngine.Random.Range(0, elementalCrystals.Length)], itemSlots[i].GetComponent<ItemSlot>().ceneterLocation, Quaternion.identity);
                crystal.transform.SetParent(parentForSpawning.transform);
                crystal.name = i.ToString();
                spawnedCrystals.Add(crystal);
                //slots.GetComponent<ItemSlot>().slotFilled = true;
                filledInventorySlots[i] = 1;
                break;
            }
        }
    }

    public void RefreshShopSlots(int index)
    {
        filledInventorySlots[index] = 0;
    }

    public void UpdateCash()
    {
        cashText.text = cash.ToString() + "$"; 
    }

    public float EnemyHpIncrees(float startHp)
    {
        return startHp * waveIndex;
    }
}
