using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned to pool!", this);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab.gameObject);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy()
    {
        if (pool.Count == 0)
        {
            GameObject newEnemy = Instantiate(enemyPrefab.gameObject);
            newEnemy.SetActive(false);
            return newEnemy;
        }

        return pool.Dequeue();
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}