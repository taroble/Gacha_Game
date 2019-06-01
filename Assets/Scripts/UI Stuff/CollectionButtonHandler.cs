using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionButtonHandler : MonoBehaviour
{
    public int itemID;
    Item item;

    public GameObject displayOverlay;
    Text displayName;
    Text displayAmountOwned;
    Image displayImage;
    Text displayDescription;

    void Start()
    {
        //Decide if the button should be interactable or not
        item = GameMaster.instance.GetItem(itemID);
        if (item.amountOwned > 0) GetComponent<Button>().interactable = true;

        //Get access to all display overlay elements
        displayName = displayOverlay.transform.Find("Name").GetComponent<Text>();
        displayAmountOwned = displayOverlay.transform.Find("Name/Amount Owned").GetComponent<Text>();
        displayImage = displayOverlay.transform.Find("Image").GetComponent<Image>();
        displayDescription = displayOverlay.transform.Find("Description").GetComponent<Text>();
    }

    public void DisplayItem()
    {
        displayOverlay.SetActive(true);
        displayName.text = item.itemName;
        displayAmountOwned.text = "Amount owned: " + item.amountOwned;
        displayImage.sprite = GameMaster.instance.itemImages[item.id];
        displayDescription.text = item.flavorText;
    }
}