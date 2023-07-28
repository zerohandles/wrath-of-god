using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] float lifetime = 7f;
    private float timer;


    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > lifetime)
        {
            DisableAllColliders();
            Destroy(gameObject, 1f);
        }
    }

    void DisableAllColliders()
    {
        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
        {
            return;
        }

        foreach (Enemy enemy in GameManager.instance.spawnManager.enemies)
        {
            if (collision.gameObject.CompareTag(enemy.tag))
            {
                GameManager.instance.ChangeScore(enemy.value);
                GameManager.instance.EnemyDeath(enemy);
            }
        }
        Destroy(collision.gameObject);
    }
}
