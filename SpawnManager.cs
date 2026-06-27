using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  
    public GameObject triangle;
    public GameObject circle;
    public GameObject square;

    public float spawnRate = 0.5f;
    public float minX = -8f;
    public float maxX = 8f;
    public float spawnY = 6f;

    public int poolSize = 20;

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        CreatePool();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnEnemies();
            timer = 0f;
        }
    }
    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddToPool(triangle);
            AddToPool(circle);
            AddToPool(square);
        }
    }
    void AddToPool(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        obj.SetActive(false);
        pool.Add(obj);
    }
    void SpawnEnemies()
    {
        GameObject obj = GetPooledObject();
        if (obj == null) return;

        float x = Random.Range(minX, maxX);
        obj.transform.position = new Vector2(x, spawnY);
        obj.SetActive(true);
        StartCoroutine(DeactivateAfterDelay(obj, 5f));
    }
    GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }
        return null;
    }
    IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
    }
}