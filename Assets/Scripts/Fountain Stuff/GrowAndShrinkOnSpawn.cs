using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAndShrinkOnSpawn : MonoBehaviour
{
    public float growTime;
    public float shrinkTime;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        transform.localScale = new Vector2(0.25f, 0.25f);

        LeanTween.alpha(gameObject, 1, growTime).setEase(LeanTweenType.easeOutCubic);
        LeanTween.scale(gameObject, new Vector2(1, 1), growTime).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            LeanTween.alpha(gameObject, 0, shrinkTime);
            LeanTween.scale(gameObject, new Vector2(0, 0), shrinkTime).setOnComplete(() =>
            {
                Destroy(gameObject);
            });
        });
    }
}