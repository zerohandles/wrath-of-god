using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private bool hitEnemy = false;

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
                hitEnemy = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Explode");

            if (!hitEnemy)
            {
                GameManager.instance.combo = 0;
                OverlayUI.instance.UpdateComboText();
            }
            // Use object pooling for lightning bolts
            Destroy(gameObject);
        }
    }
}
