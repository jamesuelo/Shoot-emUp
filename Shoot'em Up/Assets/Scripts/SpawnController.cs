using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> wavesConfigs;
    [SerializeField] private float initialWaitTime;
    [SerializeField] private float cadenceBetweenWaves;
    [SerializeField] private bool repeatAllWaves;
    private List<EnemyController> enemies = new List<EnemyController>();
    // Start is called before the first frame update
    private void Start()
    {
        if (repeatAllWaves)
        {
            StartCoroutine(EnemySpawnerEternalCoroutine());
        }
        else
        {
            StartCoroutine(EnemySpawnerCoroutine());
        }
    }
    private void OnEnemyDied(GameObject corpse)
    {
        var enemyController = corpse.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemies.Remove(enemyController);
        }
    }
    private IEnumerator EnemySpawnerCoroutine()
    {
        yield return new WaitForSeconds(initialWaitTime);
        foreach (var wave in wavesConfigs)
        {
            foreach (var enemy in wave.enemies)
            {
                Vector3 enemyPosition = Vector3.zero;
                if (enemy.useSpecificXPosition)
                {
                    enemyPosition = enemy.spawnReferencePosition;
                }
                else
                {
                    enemyPosition = new Vector3(Random.Range(-enemy.spawnReferencePosition.x, enemy.spawnReferencePosition.x), enemy.spawnReferencePosition.y, enemy.spawnReferencePosition.z);
                }
                SpawnEnemy(enemy.enemyPrefab, enemy.config, enemyPosition, enemy.rotation);
                yield return new WaitForSeconds(wave.cadence);
            }
            yield return new WaitForSeconds(cadenceBetweenWaves);
        }
    }

    public void SpawnEnemy(EnemyController enemyPrefab, EnemyConfig config, Vector3 enemyPosition, Quaternion rotation)
    {
        var enemyInstance = Instantiate(enemyPrefab, enemyPosition, rotation);
        enemyInstance.config = config;

        enemies.Add(enemyInstance);
    }

    private IEnumerator EnemySpawnerEternalCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(EnemySpawnerCoroutine());
        }
    }

}
