using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItem", menuName = "ScriptableObjects/ShopItem")]
public class ShopItem : ScriptableObject
{
    public string itemName;
    public string typeOfItem;
    public Sprite icon;
    public int price;
    public string description;
    public GameObject boughtItem;
    public bool isPassive = false;
    [Header("DONT FUCK UP THE KEY IT IS USED TO ADD PASSIV EFFECTS")]
    public string passivItemKey;
}
