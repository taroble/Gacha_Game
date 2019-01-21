using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
    Vector2 homePos;
    float shakeIntensity;

    void Start()
    {
        homePos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector2(homePos.x + Random.Range(-shakeIntensity, shakeIntensity), homePos.y + Random.Range(-shakeIntensity, shakeIntensity));
    }
}