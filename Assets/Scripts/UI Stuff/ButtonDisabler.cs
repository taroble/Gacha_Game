using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
	public Button[] buttons;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            print(i);
            Item item = GameMaster.instance.GetItem(i);

            /*if (item.amountOwned == 0)
            {
                buttons[i].interactable = false;
            }
            else
            {
                buttons[i].interactable = true;
            }*/
        }
    }
}