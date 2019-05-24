using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
	public GameObject[] pages;
    public GameObject itemBlock;
    public Text itemText;



    public void openPage(int page)
	{
		for (int i = 0; i < pages.Length; i++)
		{
			if (i == page) {
				pages[i].SetActive(true);
			}
			else {
				pages[i].SetActive(false);
			}
		}
	}

	public void closeAll()
	{
		for (int i = 0; i < pages.Length; i++)
		{
			pages[i].SetActive(false);
		}
	}

    public void GetItemCommon(int ID)
    {
        Item cItem = GameMaster.instance.GetItemCommon(ID);
        PrintValues(cItem);
    }

    public void GetItemUncommon(int ID)
    {
        Item ucItem = GameMaster.instance.GetItemUncommon(ID);
        PrintValues(ucItem);
    }

    public void GetItemRare(int ID)
    {
        Item rItem = GameMaster.instance.GetItemRare(ID);
        PrintValues(rItem);
    }

    public void GetItemUltraRare(int ID)
    {
        Item urItem = GameMaster.instance.GetItemUltraRare(ID);
        PrintValues(urItem);
    }

    void PrintValues(Item getItem)
    {
        itemBlock.SetActive(true);
        itemText.text = getItem.itemName + ": " + getItem.amountOwned + "\n" + getItem.flavorText;
        //Debug.Log(getItem.itemName + ": " + getItem.amountOwned + "\n" + getItem.flavorText);

        if (Input.GetMouseButton(0))
        {
            itemBlock.SetActive(false);
        }
    }
}
