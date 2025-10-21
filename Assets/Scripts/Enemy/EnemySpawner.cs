using System.Collections.Generic;
using Combat;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private UnitFactory UnitFactory;
    [SerializeField] private GameObject EnemyContainer;
    [SerializeField] private Transform SpawnArea;

    [SerializeField] private float startSpawnInterval = 4f;
    [SerializeField] private float endSpawnInterval = 1f;
    [SerializeField] private float SpawnIntervalDecreaseIncrement = 0.1f;
    private float currentSpawnInterval = 4f;
    private float spawnAreaBottom;

    private float spawnAreaLeft;
    private float spawnAreaRight;
    private float spawnAreaTop;

    private float timer;

    private readonly List<string> unitIds = new();

    private void Awake()
    {
        spawnAreaLeft = SpawnArea.position.x - SpawnArea.localScale.x / 2;
        spawnAreaRight = SpawnArea.position.x + SpawnArea.localScale.x / 2;
        spawnAreaTop = SpawnArea.position.y + SpawnArea.localScale.y / 2;
        spawnAreaBottom = SpawnArea.position.y - SpawnArea.localScale.y / 2;
        currentSpawnInterval = startSpawnInterval;
        foreach (var unitDefinition in UnitFactory.GetUnitDefinitions())
            if (unitDefinition.enemyPrefab != null)
                unitIds.Add(unitDefinition.id);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnInterval)
        {
            currentSpawnInterval -= SpawnIntervalDecreaseIncrement;
            if (currentSpawnInterval < endSpawnInterval) currentSpawnInterval = endSpawnInterval;

            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        var spawnPosition = new Vector3(
            Random.Range(spawnAreaLeft, spawnAreaRight),
            Random.Range(spawnAreaBottom, spawnAreaTop),
            0f
        );

        var enemyGameObject =
            UnitFactory.CreateUnit(unitIds[Random.Range(0, unitIds.Count)], spawnPosition, Faction.Enemy);
        enemyGameObject.transform.SetParent(EnemyContainer.transform);
    }
}