using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 moveDirection;
    private float moveSpeed = 5f;
    private EnemyPool pool;

    // Добавляем переменную для доступа к компоненту Animator
    private Animator animator;

    void Start()
    {
        // Получаем компонент Animator, который висит на этом же объекте
        animator = GetComponent<Animator>();

        // Важно: проверяем, есть ли он вообще (на случай, если забудешь добавить)
        if (animator == null)
        {
            Debug.LogWarning("Animator component not found on " + gameObject.name);
        }
    }

    public void Initialize(Vector3 direction, EnemyPool ownerPool)
    {
        moveDirection = direction.normalized;
        pool = ownerPool;

        // Когда враг только появился, он сразу начинает двигаться,
        // значит, скорость у него уже есть. Можно задать параметр Speed.
        if (animator != null)
        {
            animator.SetFloat("Speed", moveSpeed); // Передаём скорость в аниматор
        }
    }

    void Update()
    {
        // Движение по прямой
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    // Метод для возврата в пул (можно оставить как есть)
    void OnBecameInvisible()
    {
        ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (pool != null)
            pool.ReturnEnemy(gameObject);
        else
            Destroy(gameObject);
    }
}