using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDisable : MonoBehaviour
{
    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}