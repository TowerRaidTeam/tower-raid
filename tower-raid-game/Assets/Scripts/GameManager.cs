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
    [HideInInspector] public int[] filledInventorySlots = { 0, 0, 0 , 0, 0, 0};
    [SerializeField] GameObject[] elementalCrystals;
    [SerializeField] GameObject[] itemSlots;
    [HideInInspector] public List<GameObject> spawnedCrystals = new List<GameObject>();

    [Header("SHOP")]
    [SerializeField] ShopItem[] notInUse;
    ShopManager shopManager;
    [SerializeField] GameObject shopPanel;
    [SerializeField] TMP_Text shopMoneyView;
    [SerializeField] Transform passivItemHolder;
    [SerializeField] GameObject passivItemPrefab;
    [SerializeField] GameObject openShopButton;
    int shopRefreshIndex = 0;
    public int itemPriceIncress;

    //Passive Item Variables
    public float piggyBankInterest;
    //List<GameObject> itemsToDisplayInShop;

    private void Start()
    {
        Cursor.SetCursor(cursourTexture, Vector2.zero, CursorMode.Auto);

        gameSpeedSlider.value = 1;
        Time.timeScale = gameSpeedSlider.value;

        UpdateWaveCounter(0);
        AddCash(0);
        sortingArray = FindObjectOfType<SortingArray>();
        shopManager = GetComponent<ShopManager>();
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
        
        itemPriceIncress = waveIndex * 25;
        WorldGeneration.path = sortingArray.GenerateNewPath().ToArray();
        enemySpawnPosition = WorldGeneration.path[WorldGeneration.path.Length - 1] + Vector3.up;
        UpdateWaveCounter(1);
        cash += (int)(cash * piggyBankInterest);
        UpdateCash();
        OpenAndCloseShop(openShopButton, false);
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
        StartCoroutine(ShowShopAfterAllMinions());
    }

    IEnumerator ShowShopAfterAllMinions()
    {
        while (FindObjectsOfType<Enemy>().Length > 0)
        {
            yield return new WaitForSeconds(0.25f);
        }
        if (waveIndex >= 30)
        {
            winScreen.SetActive(true);
        }
        shopManager.OpenShopAndItems();
        OpenAndCloseShop(openShopButton, true);
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
        waveText.text = "WAVE: " + waveIndex + "/30";

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

    //public void BuyNewCrystal()
    //{
    //    if (cash >= 50)
    //    {
    //        cash -= 50;
    //        UpdateCash();
    //        foreach (var item in filledInventorySlots)
    //        {
    //            if (item == 0)
    //            {
    //                SpawnItemOnSlot();
    //                break;
    //            }
    //            else
    //            {
    //                Debug.Log("Inventory Full");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("not enaugh money");
    //    }

    //}

    //private void SpawnItemOnSlot()
    //{
    //    //valueChange = 1;

    //    for (int i = 0; i < filledInventorySlots.Length; i++)
    //    {
    //        if (filledInventorySlots[i] == 0)
    //        {
    //            GameObject item = Instantiate(elementalCrystals[UnityEngine.Random.Range(0, elementalCrystals.Length)], itemSlots[i].GetComponent<ItemSlot>().ceneterLocation, Quaternion.identity);
    //            item.transform.SetParent(parentForSpawning.transform);
    //            item.name = i.ToString();
    //            spawnedCrystals.Add(item);
    //            //slots.GetComponent<ItemSlot>().slotFilled = true;
    //            filledInventorySlots[i] = 1;
    //            break;
    //        }
    //    }
    //}
    public void BuyNewItem(GameObject itemToInstantiate)
    {
        foreach (var item in filledInventorySlots)
        {
            if (item == 0)
            {
                SpawnItemOnSlot(itemToInstantiate);
                break;
            }
            else
            {
                Debug.Log("Inventory Full");
            }
        }
    }

    public void BuyNewPassiveItem(Sprite iconToUse)
    {
        GameObject iconHolder = Instantiate(passivItemPrefab, Vector2.zero, Quaternion.identity);
        iconHolder.transform.SetParent(passivItemHolder);
        iconHolder.GetComponent<Image>().sprite = iconToUse;
    }

    private void SpawnItemOnSlot(GameObject itemToSpawn)
    {
        //valueChange = 1;

        for (int i = 0; i < filledInventorySlots.Length; i++)
        {
            if (filledInventorySlots[i] == 0)
            {
                GameObject item = Instantiate(itemToSpawn, itemSlots[i].GetComponent<ItemSlot>().ceneterLocation, Quaternion.identity);
                item.transform.SetParent(parentForSpawning.transform);
                item.name = i.ToString();
                spawnedCrystals.Add(item);
                //slots.GetComponent<ItemSlot>().slotFilled = true;
                filledInventorySlots[i] = 1;
                shopManager.DisplayInventoryInShop(spawnedCrystals.ToArray());
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
        shopMoneyView.text = cash.ToString() + "$";
    }

    public float EnemyHpIncrees(float startHp)
    {
        return startHp * (waveIndex * 0.5f) * (FindObjectsOfType<Tower>().Length * 0.5f);
    }

    public void ShowShop()
    {
        foreach (var item in spawnedCrystals)
        {
            Debug.Log("LIST: " + item);
        }
        foreach (var item in filledInventorySlots)
        {
            Debug.Log(item);
        }
        shopManager.DisplayInventoryInShop(spawnedCrystals.ToArray());
        shopManager.OpenShopAndItems();
    }

    public void UpdateShopInventory()
    {
        shopManager.DisplayInventoryInShop(spawnedCrystals.ToArray());
    }

    public void RefreshShop()
    {
        shopRefreshIndex++;
        if (cash >= 50 * shopRefreshIndex / 2)
        {
            cash -= 50 * shopRefreshIndex / 2;
            
            UpdateCash();
            shopManager.RefreshShop();
        }
    }

    public void ItemFollowHand(Transform transform)
    {
        transform.position = Input.mousePosition;
    }

    void OpenAndCloseShop(GameObject gameObject, bool isActive)
    {
        gameObject.SetActive(isActive);
        shopManager.CheckIfYouHaveEnaughrSpaceAndTurnOnPanel();

    }
}
