using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineKnob : MonoBehaviour
{
    public GameObject capsulePrefab;
    bool clickable = true;

    void OnMouseOver()
    {
        if (clickable && Input.GetMouseButtonDown(0))
        {
            clickable = false;
            Instantiate(capsulePrefab);
        }
    }
}