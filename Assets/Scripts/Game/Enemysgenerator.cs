using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemysgenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; 
    public GameObject signalPrefab; 
    public float minY = -5f; 
    public float maxY = 5f; 
    public float spawnIntervalMin = 2f; 
    public float spawnIntervalMax = 5f; 
    public float startX = 10f; 
    public float destroyX = -10f;
    public float signalOffsetX = 2f; 

    private bool isWaitingForSignal = false;
    private float currentSpawnY; 

    void Start()
    {
        StartCoroutine(SpawnEnemiesWithSignal());
    }
    IEnumerator SpawnEnemiesWithSignal()
    {
        while (true)
        {
            // Esperar la señal
            yield return StartCoroutine(ShowSignal());

            // Generar enemigo después de la señal
            GenerateEnemy(currentSpawnY);

            // Esperar un tiempo antes de volver a generar
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }
    IEnumerator ShowSignal()
    {
        isWaitingForSignal = true;
        currentSpawnY = Random.Range(minY, maxY);
        GameObject signalObject = Instantiate(signalPrefab, new Vector3(startX - signalOffsetX, currentSpawnY, 0f), Quaternion.identity);
        yield return new WaitForSeconds(1f); 
        Destroy(signalObject);
        isWaitingForSignal = false;
    }
    void GenerateEnemy(float spawnY)
    {
        if (isWaitingForSignal)
        {
            Debug.LogWarning("Aún se está esperando la señal. No se generará el enemigo.");
            return;
        }
        GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Vector3 spawnPosition = new Vector3(startX, spawnY, 0f);
        GameObject newObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-2f, 0f); // Velocidad hacia la izquierda
        }
    }
}
