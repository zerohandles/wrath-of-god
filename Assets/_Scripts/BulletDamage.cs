using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private bool hitEnemy = false;

    public GameObject explosionPoint;
    public GameObject explosionEffect;

    private GameObject effectsContainer;


    private void Start()
    {
        effectsContainer = GameObject.Find("Effects");
    }

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
                GameManager.instance.ChangeScore(enemy.value);
                GameManager.instance.UpdateUI();
                hitEnemy = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GameObject effect  = (GameObject)Instantiate(explosionEffect, explosionPoint.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 1f);

            if (!hitEnemy)
            {
                GameManager.instance.combo = 0;
                OverlayUI.instance.UpdateComboText();
            }
            Destroy(gameObject);
        }
    }
}
