using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButtonHandler : MonoBehaviour
{
    public int itemID;
    Item item;
    public GameObject descriptionOverlay;
    public Text descriptionText;

    void Start()
    {
        //Decide if the button should be interactable or not
        item = GameMaster.instance.GetItem(itemID);
        if (item.amountOwned > 0) GetComponent<Button>().interactable = true;
    }

    public void DisplayItem()
    {
        descriptionOverlay.SetActive(true);
        descriptionText.text = item.itemName + ": " + item.amountOwned + "\n" + item.flavorText;
    }
}