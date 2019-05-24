using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneClickDisappear : MonoBehaviour
{
    public GameObject menu;

    public void TurnOff(){
    	menu.SetActive(false);
    }
}
