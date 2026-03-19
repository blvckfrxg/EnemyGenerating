using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            // Расширяем пул при необходимости
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.SetActive(false);
            pool.Enqueue(newEnemy);
        }

        GameObject enemy = pool.Dequeue();
        enemy.transform.position = position;
        enemy.transform.rotation = rotation;
        enemy.SetActive(true);
        return enemy;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}