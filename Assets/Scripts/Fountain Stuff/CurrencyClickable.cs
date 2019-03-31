using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyClickable : MonoBehaviour
{
    public int value = 1;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameMaster.instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}