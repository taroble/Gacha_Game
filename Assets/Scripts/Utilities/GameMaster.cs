using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    TextAsset itemData;

    List<Item> commonItems = new List<Item>();
    List<Item> uncommonItems = new List<Item>();
    List<Item> rareItems = new List<Item>();
    List<Item> ultraRareItems = new List<Item>();
    float[] rarityChances = { 45, 30, 16, 9 };

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
        PopulateItemsArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            coins += 100;
            UpdateCoinCounter();
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



    //Columns (Left to Right):
    //0 = ID
    //1 = ITEM NAME
    //2 = CATEGORY
    //3 = RARITY
    //4 = FLAVOR TEXT
    //5 = AMOUNT OWNED
    public void PopulateItemsArray()
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
        }
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
            receivedItem = commonItems[Random.Range(0, commonItems.Count - 1)];
        }
        else if (randNum <= uncommonWeight && randNum > rareWeight)     //If 55 > randNum > 25
        {
            receivedItem = uncommonItems[Random.Range(0, uncommonItems.Count - 1)];
        }
        else if (randNum <= rareWeight && randNum > ultraRareWeight)    //If 25 > randNum > 9
        {
            receivedItem = rareItems[Random.Range(0, rareItems.Count - 1)];
        }
        else if (randNum <= ultraRareWeight && randNum > 0.001f)        //If 9 > randNum > 0
        {
            receivedItem = ultraRareItems[Random.Range(0, ultraRareItems.Count - 1)];
        }
        else
        {
            receivedItem = commonItems[Random.Range(0, commonItems.Count - 1)];
        }

        receivedItem.amountOwned++;
        //Debug.Log(receivedItem.itemName + ": " + receivedItem.amountOwned);
        return receivedItem;
    }

    public Item GetItemCommon(int ID)
    {
        for (int i = 0; i < commonItems.Count; i++)
        {
            if (commonItems[i].id == ID)
            {
                return commonItems[i];
            }
        }

        return null;
    }

    public Item GetItemUncommon(int ID)
    {
        for (int i = 0; i < uncommonItems.Count; i++)
        {
            if (uncommonItems[i].id == ID)
            {
                return uncommonItems[i];
            }
        }

        return null;
    }

    public Item GetItemRare(int ID)
    {
        for (int i = 0; i < rareItems.Count; i++)
        {
            if (rareItems[i].id == ID)
            {
                return rareItems[i];
            }
        }

        return null;
    }

    public Item GetItemUltraRare(int ID)
    {
        for (int i = 0; i < ultraRareItems.Count; i++)
        {
            if (ultraRareItems[i].id == ID)
            {
                return ultraRareItems[i];
            }
        }

        return null;
    }
}