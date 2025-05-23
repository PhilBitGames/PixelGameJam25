using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject soldierPrefab;
    [SerializeField] GameObject archerPrefab;
    [SerializeField] GameObject EnemyContainer;
    [SerializeField] Transform SpawnArea;

    [SerializeField] float startSpawnInterval = 4f;
    [SerializeField] float endSpawnInterval = 1f;
    [SerializeField] float SpawnIntervalDecreaseIncrement = 0.1f;
    
    float timer = 0;
    private float currentSpawnInterval = 4f;

    private float spawnAreaLeft;
    private float spawnAreaRight;
    private float spawnAreaTop;
    private float spawnAreaBottom;

    private GameObject[] units; 

    private void Awake()
    {
        spawnAreaLeft = SpawnArea.position.x - SpawnArea.localScale.x / 2;
        spawnAreaRight = SpawnArea.position.x + SpawnArea.localScale.x / 2;
        spawnAreaTop = SpawnArea.position.y + SpawnArea.localScale.y / 2;
        spawnAreaBottom = SpawnArea.position.y - SpawnArea.localScale.y / 2;
        currentSpawnInterval = startSpawnInterval;
        units = new []{ soldierPrefab, archerPrefab };
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            currentSpawnInterval -= SpawnIntervalDecreaseIncrement;
            if (currentSpawnInterval < endSpawnInterval)
            {
                currentSpawnInterval = endSpawnInterval;
            }
        
            SpawnEnemy();
            timer = 0;
            
            // if()
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = new Vector3(
            UnityEngine.Random.Range(spawnAreaLeft, spawnAreaRight),
            UnityEngine.Random.Range(spawnAreaBottom, spawnAreaTop),
            0f
        );
        Instantiate(units[Random.Range(0,units.Length)], spawnPosition, Quaternion.identity, EnemyContainer.transform);
    }
}
