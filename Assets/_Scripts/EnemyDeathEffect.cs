using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathEffect : MonoBehaviour
{
    public GameObject deathEffect;

    [HideInInspector] public ObjectPool<GameObject> prefabPool;
    [HideInInspector] public ObjectPool<GameObject> effectPool;

    void DisableGameObject()
    {
        prefabPool.Release(gameObject);
    }

    void Death()
    {
        if (gameObject.CompareTag("Innocent"))
        {
            GameManager.instance.innocentsKilled += 1;
        }

        GameManager.instance.spawnManager.SpawnEfect(effectPool, gameObject.transform.position, 3);
        DisableGameObject();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Lightning") || collision.gameObject.CompareTag("Explosion"))
        {
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tornado"))
        {
            Death();
        }
    }
}
