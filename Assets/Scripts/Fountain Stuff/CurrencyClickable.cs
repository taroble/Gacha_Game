﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyClickable : MonoBehaviour
{
    public int value = 1;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameMaster.instance.quarters += value;
            Destroy(gameObject);
        }
    }
}