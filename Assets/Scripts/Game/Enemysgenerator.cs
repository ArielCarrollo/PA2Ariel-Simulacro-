using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemysgenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Los prefabs que deseas instanciar
    public GameObject signalPrefab; // El prefab de la señal
    public float minY = -5f; // Rango mínimo en Y
    public float maxY = 5f; // Rango máximo en Y
    public float spawnIntervalMin = 2f; // Intervalo mínimo entre instancias
    public float spawnIntervalMax = 5f; // Intervalo máximo entre instancias
    public float startX = 10f; // Posición inicial en X
    public float destroyX = -10f; // Posición en X donde se destruyen los objetos
    public float signalOffsetX = 2f; // Offset en X para la posición de la señal

    private bool isWaitingForSignal = false; // Variable para controlar si se está esperando la señal
    private float currentSpawnY; // Almacena la posición Y de generación del enemigo actual

    void Start()
    {
        // Iniciar la generación de enemigos
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

        // Generar posición Y aleatoria
        currentSpawnY = Random.Range(minY, maxY);

        // Instanciar la señal en una posición X menos visible
        GameObject signalObject = Instantiate(signalPrefab, new Vector3(startX - signalOffsetX, currentSpawnY, 0f), Quaternion.identity);

        // Esperar un tiempo antes de generar el enemigo
        yield return new WaitForSeconds(1f); // Por ejemplo, espera 1 segundo

        // Destruir la señal
        Destroy(signalObject);

        isWaitingForSignal = false;
    }

    void GenerateEnemy(float spawnY)
    {
        // Verificar si se está esperando la señal
        if (isWaitingForSignal)
        {
            Debug.LogWarning("Aún se está esperando la señal. No se generará el enemigo.");
            return;
        }

        // Instanciar un enemigo
        GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Vector3 spawnPosition = new Vector3(startX, spawnY, 0f);
        GameObject newObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        // Configurar la dirección del movimiento hacia la izquierda
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-2f, 0f); // Velocidad hacia la izquierda
        }
    }
}
