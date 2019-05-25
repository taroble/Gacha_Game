using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisablerTwo : MonoBehaviour
{
    public enum Rarity { Common, Uncommon, Rare, UltraRare };
    public Rarity rarity;
    public int itemID;

    void Start()
    {
        switch (rarity)
        {
            case Rarity.Common:
                if (GameMaster.instance.GetItemCommon(itemID).amountOwned > 0)
                {
                    GetComponent<Button>().interactable = true;
                }
                break;

            case Rarity.Uncommon:
                if (GameMaster.instance.GetItemUncommon(itemID).amountOwned > 0)
                {
                    GetComponent<Button>().interactable = true;
                }
                break;

            case Rarity.Rare:
                if (GameMaster.instance.GetItemRare(itemID).amountOwned > 0)
                {
                    GetComponent<Button>().interactable = true;
                }
                break;

            case Rarity.UltraRare:
                if (GameMaster.instance.GetItemUltraRare(itemID).amountOwned > 0)
                {
                    GetComponent<Button>().interactable = true;
                }
                break;
        }
    }
}