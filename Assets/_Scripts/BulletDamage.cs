using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
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
                GameManager.instance.score += enemy.value;
                OverlayUI.Instance.UpdateScore();
                // GameObject effect = (GameObject)Instantiate(enemy.deathEffect, collision.transform.position, Quaternion.identity);
            }
        }

        // Destroy(collision.gameObject);
    }
}
