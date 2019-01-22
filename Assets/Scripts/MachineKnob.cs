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
            LeanTween.rotateZ(gameObject, -720, 1).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                Instantiate(capsulePrefab);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            });
        }
    }
}