using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveDirection;
    
    void Start()
    {
        moveDirection = (Random.insideUnitCircle * 5f).normalized;
    }

    void FixedUpdate()
    {
        float rotationSpeed = Random.Range(-100f, 100f);
        rb.angularVelocity = rotationSpeed;
        
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)moveDirection);
    }
}
