using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject itemShopHolder;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject[] inventoryItem;
    [SerializeField] GameObject inventoryFullPanel;

    ItemManager[] allChldren;

    [SerializeField] Sprite emptyCell;
    GameManager gm;
    public List<string> passivItemKeys = new List<string>();

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void OpenShopAndItems()
    {
        if (!CheckIfInventoryHasSpace(gm.filledInventorySlots))
        {
            inventoryFullPanel.SetActive(true);
            allChldren = itemShopHolder.GetComponentsInChildren<ItemManager>();
            foreach (ItemManager item in allChldren)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject item = Instantiate(itemPrefab, Vector2.zero, Quaternion.identity);
                item.transform.SetParent(itemShopHolder.transform);
            }
            shopPanel.SetActive(true);
        }
        else
        {
            inventoryFullPanel.SetActive(false);
            allChldren = itemShopHolder.GetComponentsInChildren<ItemManager>();
            foreach (ItemManager item in allChldren)
            {
                Destroy(item.gameObject);
            }
            for (int i = 0; i < 4; i++)
            {
                GameObject item = Instantiate(itemPrefab, Vector2.zero, Quaternion.identity);
                item.transform.SetParent(itemShopHolder.transform);
            }
            shopPanel.SetActive(true);
        }
    }

    public void RefreshShop()
    {
        allChldren = itemShopHolder.GetComponentsInChildren<ItemManager>();
        foreach (ItemManager item in allChldren)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject item = Instantiate(itemPrefab, Vector2.zero, Quaternion.identity);
            item.transform.SetParent(itemShopHolder.transform);
        }
    }

    //This parameter does nothing i just didnt whatn to change it everywhere
    public void DisplayInventoryInShop(GameObject[] items)
    {
        //for (int i = 0; i < gm.filledInventorySlots.Length; i++)
        //{
        //    if (gm.filledInventorySlots[i] == 0)
        //    {
        //        inventoryItem[i].GetComponent<Image>().sprite = emptyCell;
        //    }
        //    else
        //    {
        //        inventoryItem[i].GetComponent<Image>().sprite = items[i].GetComponent<Image>().sprite;
        //    }
        //}

        for (int i = 0; i < items.Length; i++)
        {
            inventoryItem[i].GetComponent<Image>().sprite = items[i].GetComponent<Image>().sprite;
        }
    }

    public bool CheckIfInventoryHasSpace(int[] array)
    {
        bool returnBool = true;
        foreach (int slot in array)
        {
            if (slot == 0)
            {
                returnBool = true;
                break;
            }
            else
            {
                returnBool = false;
            }
        }
        return returnBool;
    }


    public void CheckIfYouHaveEnaughrSpaceAndTurnOnPanel()
    {
        if (!CheckIfInventoryHasSpace(gm.filledInventorySlots))
        {
            inventoryFullPanel.SetActive(true);
        }
        else
        {
            inventoryFullPanel.SetActive(false);
        }
    }
}
