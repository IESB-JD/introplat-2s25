using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private GameObject smallAsteroidPrefab;
    [SerializeField] private float health = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveDirection;
    private float _rotationSpeed;
    
    [SerializeField] private bool isSmall = false;
    
    void Start()
    {
        if (!isSmall)
        {
            moveDirection = (Random.insideUnitCircle * 5f).normalized;
        }
        //_rotationSpeed = Random.Range(-100f, 100f);
    }
    
    public void Initialize(Vector2 direction)
    {
        moveDirection = direction;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = _rotationSpeed;
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Collided with a bullet!");
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    private void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            if(smallAsteroidPrefab != null)
            {
                SpawnSmallerAsteroids();
            }
        }
    }

    private void SpawnSmallerAsteroids()
    {
        Vector2 currentVelocity = rb.linearVelocity;
        
        //Vector2 perpendicularDirection = new Vector2(-currentVelocity.y, currentVelocity.x).normalized;
        Vector2 perpendicularDirection = Vector2.Perpendicular(currentVelocity).normalized;
        
        Vector2 direction1 = (currentVelocity.normalized + perpendicularDirection).normalized;
        Vector2 direction2 = (currentVelocity.normalized - perpendicularDirection).normalized;

        var small1 = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        var small2 = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        
        small1.GetComponent<AsteroidController>().Initialize(direction1);
        small2.GetComponent<AsteroidController>().Initialize(direction2);
    }
}
