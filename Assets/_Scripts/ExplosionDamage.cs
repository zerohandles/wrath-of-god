using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
        {
            return;
        }

        foreach (Enemy enemy in GameManager.instance.spawnManager.enemies)
        {
            if (collision.gameObject.CompareTag(enemy.tag))
            {
                GameManager.instance.EnemyDeath(enemy);
            }
        }
    }
}
