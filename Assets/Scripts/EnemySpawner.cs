using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPool enemyPool;            // Ссылка на пул
    public float spawnInterval = 2f;       // Интервал спавна
    public List<Transform> spawnPoints;    // Список точек спавна (задаётся в инспекторе)

    void Start()
    {
        // Запускаем повторяющийся спавн
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("Нет точек спавна!");
            return;
        }

        // Выбираем случайную точку
        Transform selectedPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        // Получаем направление (например, вперёд от точки)
        Vector3 direction = selectedPoint.forward;

        // Создаём врага через пул
        GameObject enemyObj = enemyPool.GetEnemy(selectedPoint.position, selectedPoint.rotation);

        // Инициализируем врага (передаём направление и ссылку на пул)
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Initialize(direction, enemyPool);
        }
    }
}