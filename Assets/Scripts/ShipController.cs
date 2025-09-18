using System;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float rotationSpeed = 200f;
    public Rigidbody2D rb;

    private float moveInput = 0;
    private float rotationInput = 0;
    
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
    }
    
    void FixedUpdate()
    {
        Vector2 moveDirection = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotate the ship
        float rotation = -rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Collided with an asteroid!");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
