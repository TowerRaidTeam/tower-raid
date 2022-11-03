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
    ShopManager sm;
    [SerializeField] GameObject itemSoldPanel;
    
    

    private void Start()
    {
        sm = FindObjectOfType<ShopManager>();
        gm = FindObjectOfType<GameManager>();
        if (items != null)
        {
            itemTOUse = items[Random.Range(0, items.Length)];
            itemName.text = itemTOUse.itemName;
            price.text = (itemTOUse.price + gm.itemPriceIncress - (int)((itemTOUse.price + gm.itemPriceIncress) * gm.discountNew)).ToString() + "$";
            description.text = itemTOUse.description;
            icon.sprite = itemTOUse.icon;
            type.text = itemTOUse.typeOfItem;
        }
    }

    public void BuyItem()
    {
        if (itemTOUse.price + gm.itemPriceIncress <= gm.cash)
        {
            if (itemTOUse.isPassive == true)
            {
                gm.cash -= itemTOUse.price + gm.itemPriceIncress - (int)((itemTOUse.price + gm.itemPriceIncress) * gm.discountNew);
                gm.UpdateCash();
                sm.passivItemKeys.Add(itemTOUse.passivItemKey);
                Debug.Log("BOUGHT NEW ITEM");
                gm.BuyNewPassiveItem(itemTOUse.icon);
                itemSoldPanel.SetActive(true);
                AddPassivEffects();
            }
            else
            {
                gm.cash -= itemTOUse.price + gm.itemPriceIncress - (int)((itemTOUse.price + gm.itemPriceIncress) * gm.discountNew);
                gm.UpdateCash();
                Debug.Log("BOUGHT NEW ITEM");
                gm.BuyNewItem(itemTOUse.boughtItem);
                itemSoldPanel.SetActive(true);
                sm.CheckIfYouHaveEnaughrSpaceAndTurnOnPanel();
            }
        }
        
    }

    void AddPassivEffects()
    {
        foreach (string item in sm.passivItemKeys)
        {
            switch (item)
            {
                case "PiggyBank": //Interest on all money when you start the wave
                    gm.piggyBankInterest += 0.05f;
                    Debug.Log("ADDED PIGGY BANK EFFECT");
                    sm.passivItemKeys.Remove(item);
                    break;
                case "Discount": //Discount on all items
                    gm.discountNew += 0.05f;
                    Debug.Log(gm.discountNew);
                    UpdatePrice();
                    sm.RefreshPrices();
                    UpdatePrice();
                    sm.passivItemKeys.Remove(item);
                    break;
                case "Harvest": //More money per kill
                    gm.harvest += 5;
                    Debug.Log(gm.harvest);
                    UpdatePrice();
                    sm.RefreshPrices();
                    UpdatePrice();
                    sm.passivItemKeys.Remove(item);
                    break;
                default:
                    Debug.Log("NO ITEM IN LIST IN ITEM MANAGER");
                    break;
            }
        }
    } 

    public void UpdatePrice()
    {
        price.text = (itemTOUse.price + gm.itemPriceIncress - (int)((itemTOUse.price + gm.itemPriceIncress) * gm.discountNew)).ToString() + "$";
    }

}
