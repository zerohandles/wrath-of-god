using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathEffect : MonoBehaviour
{
    ObjectPool<GameObject> prefabPool;
    ObjectPool<GameObject> effectPool;

    void Death()
    {
        // Tracks any "innocent" enemies killed by the player
        if (gameObject.CompareTag("Innocent"))
            GameManager.Instance.innocentsKilled += 1;

        // Get a death particle effect from the effect pool
        GameManager.Instance.spawnManager.SpawnEfect(effectPool, gameObject.transform.position, 3);
        if (gameObject.activeSelf)
            prefabPool.Release(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Lightning") || collision.gameObject.CompareTag("Explosion"))
            Death();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tornado"))
            Death();
    }

    public void SetPool(ObjectPool<GameObject> pool, ObjectPool<GameObject> deathEffectPool)
    {
        prefabPool = pool;
        effectPool = deathEffectPool;
    }
}
