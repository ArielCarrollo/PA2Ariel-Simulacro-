using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candyplusgenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn;
    public float minY = -5f;
    public float maxY = 5f;
    public float spawnIntervalMin = 1f;
    public float spawnIntervalMax = 3f;
    public float startX = 10f;
    public float destroyX = -10f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
            Vector3 spawnPosition = new Vector3(startX, Random.Range(minY, maxY), 0f);
            GameObject newObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(-2f, 0f);
            }

            nextSpawnTime += Random.Range(spawnIntervalMin, spawnIntervalMax);
        }
    }
}
