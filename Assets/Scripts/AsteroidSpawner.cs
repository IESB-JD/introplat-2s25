using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2f;
    public float initialDelay = 1f;
    public int initialCount = 3;
    public int maxCount = 10;

    public int count;
    
    private void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            SpawnAsteroid();
        }
        
        InvokeRepeating("SpawnAsteroid", initialDelay, spawnInterval);
    }
    
    private void SpawnAsteroid()
    {
        Instantiate(asteroidPrefab, GetRandomPosition(), Quaternion.identity);
    }

    private Vector2 GetRandomPosition()
    {
        float x = Random.Range(0, Screen.width);
        float y = Random.Range(0, Screen.height);
        Vector2 screenPosition = new Vector2(x, y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
