using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 moveDirection;
    private EnemyPool pool;
    private Animator animator;
    private int speedHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        speedHash = Animator.StringToHash("Speed");
    }

    public void Initialize(Vector3 direction, EnemyPool ownerPool)
    {
        moveDirection = direction.normalized;
        pool = ownerPool;

        if (animator != null)
        {
            animator.SetFloat(speedHash, moveSpeed);
        }
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
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