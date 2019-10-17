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

    float[] rarityChances = { 75.0f, 20.0f, 4.9f, 0.1f };
    //Index 0: Common chance
    //Index 1: Uncommon chance
    //Index 2: Rare chance
    //Index 3: Ultra-Rare chance

    Dictionary<int, Item> items = new Dictionary<int, Item>();

    public Sprite[] itemImages;



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
            PlayerPrefs.SetInt("Coin Amount", PlayerPrefs.GetInt("Coin Amount", 0) + 100);
            UpdateCoinCounter();
        }
    }



    //Columns (Left to Right):
    //0 = ID
    //1 = ITEM NAME
    //2 = CATEGORY
    //3 = RARITY
    //4 = FLAVOR TEXT
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
            newItem.image = itemImages[i - 1];

            items.Add(newItem.id, newItem);
        }
    }



    public int GetCoinAmount()
    {
        return PlayerPrefs.GetInt("Coin Amount", 0);
    }

    public void AddCoins(int amountOfCoins)
    {
        int coins = PlayerPrefs.GetInt("Coin Amount", 0);
        PlayerPrefs.SetInt("Coin Amount", coins + amountOfCoins);
        UpdateCoinCounter();
    }

    public void SubtractCoins(int amountOfCoins)
    {
        int coins = PlayerPrefs.GetInt("Coin Amount", 0);
        PlayerPrefs.SetInt("Coin Amount", coins - amountOfCoins);
        UpdateCoinCounter();
    }

    public void UpdateCoinCounter()
    {
        int coins = PlayerPrefs.GetInt("Coin Amount", 0);
        GameObject.FindGameObjectWithTag("Coin Counter").GetComponent<TextMeshProUGUI>().text = coins.ToString();
    }



    public Item[] GrabRandomItems(int amount)
    {
        Item[] receivedItems = new Item[amount];

        float commonWeight = rarityChances[0] + rarityChances[1] + rarityChances[2] + rarityChances[3];
        float uncommonWeight = rarityChances[1] + rarityChances[2] + rarityChances[3];
        float rareWeight = rarityChances[2] + rarityChances[3];
        float ultraRareWeight = rarityChances[3];

        for (int i = 0; i < amount; i++)
        {
            float randNum = Random.Range(0f, 100f);
            if (randNum <= commonWeight && randNum > uncommonWeight)
            {
                receivedItems[i] = commonItems[Random.Range(0, commonItems.Count)];
            }
            else if (randNum <= uncommonWeight && randNum > rareWeight)
            {
                receivedItems[i] = uncommonItems[Random.Range(0, uncommonItems.Count)];
            }
            else if (randNum <= rareWeight && randNum > ultraRareWeight)
            {
                receivedItems[i] = rareItems[Random.Range(0, rareItems.Count)];
            }
            else if (randNum <= ultraRareWeight && randNum > 0.001f)
            {
                receivedItems[i] = ultraRareItems[Random.Range(0, ultraRareItems.Count)];
            }
            else
            {
                receivedItems[i] = commonItems[Random.Range(0, commonItems.Count)];
            }

            PlayerPrefs.SetInt(receivedItems[i].itemName, PlayerPrefs.GetInt(receivedItems[i].itemName, 0) + 1);
            //print(PlayerPrefs.GetInt(receivedItems[i].itemName));
        }

        return receivedItems;
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