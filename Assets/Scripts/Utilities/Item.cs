using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string itemName;
    public string category;
    public enum Rarity { Common, Uncommon, Rare, UltraRare };
    public Rarity rarity;
    public string flavorText;
    public int amountOwned;
    public Sprite image;
}