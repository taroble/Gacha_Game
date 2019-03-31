using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDriftMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float minVelocity;
    public float maxVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(minVelocity, maxVelocity), Random.Range(minVelocity, maxVelocity));
    }
}