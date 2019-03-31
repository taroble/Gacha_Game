using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleThroughColors : MonoBehaviour
{
    public Color[] colors;
    public float cycleTimerLength = 0.1f;
    float cycleTimer;

    int currentColor;

    SpriteRenderer sr;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cycleTimer = cycleTimerLength;
    }

    void Update()
    {
        cycleTimer -= Time.deltaTime;
        if (cycleTimer <= 0)
        {
            cycleTimer += cycleTimerLength;
            currentColor++;
            if (currentColor >= colors.Length) currentColor = 0;
            sr.color = new Color(colors[currentColor].r, colors[currentColor].g, colors[currentColor].b, sr.color.a);
        }
    }
}