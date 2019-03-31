using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDestroyAnimation : MonoBehaviour
{
    public float animationTime;
    bool clickable = true;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && clickable)
        {
            clickable = false;

            LeanTween.alpha(gameObject, 0, animationTime).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
            LeanTween.scale(gameObject, new Vector2(1.5f, 1.5f), animationTime).setEase(LeanTweenType.easeOutCubic);
        }
    }
}