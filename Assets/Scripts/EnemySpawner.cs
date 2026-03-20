using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPool;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private List<Transform> spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        Transform selectedPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject enemyObj = enemyPool.GetEnemy();
        enemyObj.transform.position = selectedPoint.position;
        enemyObj.transform.rotation = selectedPoint.rotation;
        enemyObj.SetActive(true);

        if (enemyObj.TryGetComponent(out Enemy enemy))
        {
            enemy.Initialize(selectedPoint.forward, enemyPool);
        }
        else
        {
            Debug.LogError("Enemy prefab does not have Enemy component!", enemyObj);
        }
    }
}