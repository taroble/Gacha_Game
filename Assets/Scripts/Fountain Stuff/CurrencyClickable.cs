using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyClickable : MonoBehaviour
{
    public int value = 1;
    bool clickable = true;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && clickable)
        {
            clickable = false;
            GameMaster.instance.AddCoins(value);
        }
    }
}