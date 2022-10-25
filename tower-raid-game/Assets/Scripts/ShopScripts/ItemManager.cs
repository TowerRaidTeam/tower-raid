using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text type;
    [SerializeField] TMP_Text price;
    [SerializeField] TMP_Text description;
    [SerializeField] Image icon;

    [SerializeField] ShopItem[] items;
    ShopItem itemTOUse;

    GameManager gm;
    [SerializeField] GameObject itemSoldPanel;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (items != null)
        {
            itemTOUse = items[Random.Range(0, items.Length)];
            itemName.text = itemTOUse.itemName;
            price.text = itemTOUse.price.ToString() + "$";
            description.text = itemTOUse.description;
            icon.sprite = itemTOUse.icon;
            type.text = itemTOUse.typeOfItem;
        }
    }

    public void BuyItem()
    {
        if (itemTOUse.price <= gm.cash)
        {
            gm.cash -= itemTOUse.price;
            gm.UpdateCash();
            Debug.Log("BOUGHT NEW ITEM");
            gm.BuyNewItem(itemTOUse.boughtItem);
            itemSoldPanel.SetActive(true);
        }
        
    }
}
