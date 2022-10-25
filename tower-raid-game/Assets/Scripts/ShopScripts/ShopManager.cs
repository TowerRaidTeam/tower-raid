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

    ItemManager[] allChldren;

    

    public void OpenShopAndItems()
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
        shopPanel.SetActive(true);
    }

    public void DisplayInventoryInShop(GameObject[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            inventoryItem[i].GetComponent<Image>().sprite = items[i].GetComponent<Image>().sprite;
        }
    }
    
}
