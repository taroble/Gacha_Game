using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CapsuleTextAnimation : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public float translateY;

    void Start()
    {
        tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 0);
        LeanTween.moveY(gameObject, transform.position.y + translateY, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, transform.localScale * 4, 1.5f).setEase(LeanTweenType.easeOutCubic);
        LeanTween.value(0, 1, 0.5f).setEase(LeanTweenType.easeOutCubic).setOnUpdate((float value) =>
        {
            tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, value);
        });
    }
}