using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    TextAsset itemData;

    List<Item> commonItems = new List<Item>();
    List<Item> uncommonItems = new List<Item>();
    List<Item> rareItems = new List<Item>();
    List<Item> ultraRareItems = new List<Item>();
    float[] rarityChances = { 45, 30, 16, 9 };

    Dictionary<int, Item> items = new Dictionary<int, Item>();

    public Sprite[] itemImages;

    int coins;



    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(this);

        itemData = Resources.Load<TextAsset>("Item Database");
        PopulateAllDataStructures();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            coins += 100;
            UpdateCoinCounter();
        }
    }



    //Columns (Left to Right):
    //0 = ID
    //1 = ITEM NAME
    //2 = CATEGORY
    //3 = RARITY
    //4 = FLAVOR TEXT
    //5 = AMOUNT OWNED
    public void PopulateAllDataStructures()
    {
        string[] data = itemData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            Item newItem = new Item();
            int.TryParse(row[0], out newItem.id);
            newItem.itemName = row[1];
            newItem.category = row[2];

            switch (row[3])
            {
                case "Common":
                    newItem.rarity = Item.Rarity.Common;
                    commonItems.Add(newItem);
                    break;
                case "Uncommon":
                    newItem.rarity = Item.Rarity.Uncommon;
                    uncommonItems.Add(newItem);
                    break;
                case "Rare":
                    newItem.rarity = Item.Rarity.Rare;
                    rareItems.Add(newItem);
                    break;
                case "Ultra Rare":
                    newItem.rarity = Item.Rarity.UltraRare;
                    ultraRareItems.Add(newItem);
                    break;
            }

            newItem.flavorText = row[4];
            int.TryParse(row[5], out newItem.amountOwned);
            newItem.image = itemImages[i - 1];

            items.Add(newItem.id, newItem);
        }
    }



    public int GetCoinAmount()
    {
        return coins;
    }

    public void AddCoins(int amountOfCoins)
    {
        coins += amountOfCoins;
        UpdateCoinCounter();
    }

    public void SubtractCoins(int amountOfCoins)
    {
        coins -= amountOfCoins;
        UpdateCoinCounter();
    }

    public void UpdateCoinCounter()
    {
        GameObject.FindGameObjectWithTag("Coin Counter").GetComponent<TextMeshProUGUI>().text = coins.ToString();
    }



    public Item GrabRandomItem()
    {
        Item receivedItem;

        float commonWeight = rarityChances[0] + rarityChances[1] + rarityChances[2] + rarityChances[3];
        float uncommonWeight = rarityChances[1] + rarityChances[2] + rarityChances[3];
        float rareWeight = rarityChances[2] + rarityChances[3];
        float ultraRareWeight = rarityChances[3];

        float randNum = Random.Range(0f, 100f);
        if (randNum <= commonWeight && randNum > uncommonWeight)        //If 100 > randNum > 55
        {
            receivedItem = commonItems[Random.Range(0, commonItems.Count)];
        }
        else if (randNum <= uncommonWeight && randNum > rareWeight)     //If 55 > randNum > 25
        {
            receivedItem = uncommonItems[Random.Range(0, uncommonItems.Count)];
        }
        else if (randNum <= rareWeight && randNum > ultraRareWeight)    //If 25 > randNum > 9
        {
            receivedItem = rareItems[Random.Range(0, rareItems.Count)];
        }
        else if (randNum <= ultraRareWeight && randNum > 0.001f)        //If 9 > randNum > 0
        {
            receivedItem = ultraRareItems[Random.Range(0, ultraRareItems.Count)];
        }
        else
        {
            receivedItem = commonItems[Random.Range(0, commonItems.Count)];
        }

        receivedItem.amountOwned++;
        //Debug.Log(receivedItem.itemName + ": " + receivedItem.amountOwned);
        return receivedItem;
    }

    public Item GetItem(int itemID)
    {
        Item temp = null;
        if (items.TryGetValue(itemID, out temp))
        {
            return items[itemID];
        }

        return null;
    }
}