using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
    Vector3 homePos;
    float shakeIntensity;
    float shakeDegradeRate = 1f;

    void Start()
    {
        homePos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(homePos.x + Random.Range(-shakeIntensity, shakeIntensity), homePos.y + Random.Range(-shakeIntensity, shakeIntensity), transform.position.z);
        if (shakeIntensity > 0) shakeIntensity -= (shakeDegradeRate * Time.deltaTime);
        else shakeIntensity = 0;
    }

    public void ShakeCamera(float intensity, float degradeRate)
    {
        shakeIntensity = intensity;
        shakeDegradeRate = degradeRate;
    }
}