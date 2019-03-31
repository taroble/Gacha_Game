using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttraction : MonoBehaviour
{
    public float attractionForce = 10;
    public float speedLimit = 10;

    Rigidbody2D rb;
    Camera mainCamera;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (rb.velocity.x > speedLimit)
            rb.velocity = new Vector2(speedLimit, rb.velocity.y);
        if (rb.velocity.x < -speedLimit)
            rb.velocity = new Vector2(-speedLimit, rb.velocity.y);
        if (rb.velocity.y > speedLimit)
            rb.velocity = new Vector2(rb.velocity.x, speedLimit);
        if (rb.velocity.y < -speedLimit)
            rb.velocity = new Vector2(rb.velocity.x, -speedLimit);
    }

    void FixedUpdate()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldMousePos = mainCamera.ScreenToWorldPoint(mousePos);

        float angle = Mathf.Atan2(worldMousePos.y - transform.position.y, worldMousePos.x - transform.position.x) * Mathf.Rad2Deg;
        Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;
        rb.AddForce(dir * attractionForce);
    }
}