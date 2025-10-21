using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject soldierPrefab;
    [SerializeField] private GameObject archerPrefab;
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

    private GameObject[] units;

    private void Awake()
    {
        spawnAreaLeft = SpawnArea.position.x - SpawnArea.localScale.x / 2;
        spawnAreaRight = SpawnArea.position.x + SpawnArea.localScale.x / 2;
        spawnAreaTop = SpawnArea.position.y + SpawnArea.localScale.y / 2;
        spawnAreaBottom = SpawnArea.position.y - SpawnArea.localScale.y / 2;
        currentSpawnInterval = startSpawnInterval;
        units = new[] { soldierPrefab, archerPrefab };
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
        Instantiate(units[Random.Range(0, units.Length)], spawnPosition, Quaternion.identity, EnemyContainer.transform);
    }
}