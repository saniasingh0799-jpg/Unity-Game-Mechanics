using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall_spawn : _Ability
{
    public GameObject wallPrefab;
    public Transform firePoint;

    public int maxWalls = 2;
    public float wallLifeTime = 20f;
    public float cooldownTime = 30f;

    private Queue<GameObject> wallPool = new Queue<GameObject>();
    private int spawnedWalls = 0;
    private bool onCooldown = false;

    void Start()
    {
        for (int i = 0; i < maxWalls; i++)
        {
            GameObject wall = Instantiate(wallPrefab);
            wall.SetActive(false);
            wallPool.Enqueue(wall);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Activate();
        }
    }

    public override void Activate()
    {
        if (onCooldown)
            return;

        if (spawnedWalls < maxWalls)
        {
            SpawnWall();
            spawnedWalls++;

            if (spawnedWalls >= maxWalls)
            {
                StartCoroutine(Cooldown());
            }
        }
    }

    void SpawnWall()
    {
        if (wallPool.Count == 0)
            return;

        GameObject wall = wallPool.Dequeue();
        wall.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        wall.SetActive(true);

        StartCoroutine(ReturnWall(wall));
    }

    IEnumerator ReturnWall(GameObject wall)
    {
        yield return new WaitForSeconds(wallLifeTime);

        wall.SetActive(false);
        wallPool.Enqueue(wall);
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTime);

        spawnedWalls = 0;
        onCooldown = false;
    }
}
