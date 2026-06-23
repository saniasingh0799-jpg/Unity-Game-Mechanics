using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public Transform player;
    public float spawnDistance = 20f;
    public float spawnInterval = 1.5f;
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(-3f, 3f);
        Vector3 spawnPos = new Vector3(randomX, player.position.y + spawnDistance, 0);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}