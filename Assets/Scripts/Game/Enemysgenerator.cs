using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemysgenerator : MonoBehaviour
{
    public GameObject[] prefabsToSpawn; // Los prefabs que deseas instanciar
    public GameObject signalPrefab; // El prefab de la se�al
    public float minY = -5f; // Rango m�nimo en Y
    public float maxY = 5f; // Rango m�ximo en Y
    public float spawnIntervalMin = 2f; // Intervalo m�nimo entre instancias
    public float spawnIntervalMax = 5f; // Intervalo m�ximo entre instancias
    public float startX = 10f; // Posici�n inicial en X
    public float destroyX = -10f; // Posici�n en X donde se destruyen los objetos
    public float signalOffsetX = 2f; // Offset en X para la posici�n de la se�al

    private bool isWaitingForSignal = false; // Variable para controlar si se est� esperando la se�al
    private float currentSpawnY; // Almacena la posici�n Y de generaci�n del enemigo actual

    void Start()
    {
        // Iniciar la generaci�n de enemigos
        StartCoroutine(SpawnEnemiesWithSignal());
    }

    IEnumerator SpawnEnemiesWithSignal()
    {
        while (true)
        {
            // Esperar la se�al
            yield return StartCoroutine(ShowSignal());

            // Generar enemigo despu�s de la se�al
            GenerateEnemy(currentSpawnY);

            // Esperar un tiempo antes de volver a generar
            yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
        }
    }

    IEnumerator ShowSignal()
    {
        isWaitingForSignal = true;

        // Generar posici�n Y aleatoria
        currentSpawnY = Random.Range(minY, maxY);

        // Instanciar la se�al en una posici�n X menos visible
        GameObject signalObject = Instantiate(signalPrefab, new Vector3(startX - signalOffsetX, currentSpawnY, 0f), Quaternion.identity);

        // Esperar un tiempo antes de generar el enemigo
        yield return new WaitForSeconds(1f); // Por ejemplo, espera 1 segundo

        // Destruir la se�al
        Destroy(signalObject);

        isWaitingForSignal = false;
    }

    void GenerateEnemy(float spawnY)
    {
        // Verificar si se est� esperando la se�al
        if (isWaitingForSignal)
        {
            Debug.LogWarning("A�n se est� esperando la se�al. No se generar� el enemigo.");
            return;
        }

        // Instanciar un enemigo
        GameObject randomPrefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
        Vector3 spawnPosition = new Vector3(startX, spawnY, 0f);
        GameObject newObject = Instantiate(randomPrefab, spawnPosition, Quaternion.identity);

        // Configurar la direcci�n del movimiento hacia la izquierda
        Rigidbody2D rb = newObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(-2f, 0f); // Velocidad hacia la izquierda
        }
    }
}
