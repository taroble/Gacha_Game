using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnSpawn : MonoBehaviour
{
    public float growTime;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        float targetScaleX = transform.localScale.x;
        float targetScaleY = transform.localScale.y;
        transform.localScale = new Vector2(0.25f, 0.25f);

        LeanTween.alpha(gameObject, 1, growTime).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, new Vector2(targetScaleX, targetScaleY), growTime).setEase(LeanTweenType.easeOutCubic);
    }
}