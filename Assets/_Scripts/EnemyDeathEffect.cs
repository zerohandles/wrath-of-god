using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyDeathEffect : MonoBehaviour
{
    public GameObject deathEffect;
    private GameObject effectsContainer;

    [HideInInspector] public ObjectPool<GameObject> pool;

    private void Start()
    {
        effectsContainer = GameObject.Find("Effects");
    }

    public void Death()
    {
        GameManager.instance.spawnManager.KillEnemy(pool, this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Lightning") || collision.gameObject.CompareTag("Explosion"))
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 5f);
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tornado"))
        {
            GameObject effect = (GameObject)Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 5f);
            Death();
        }
    }
}
