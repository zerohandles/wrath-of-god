using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private bool hitEnemy = false;

    public GameObject explosionPoint;
    public GameObject explosionEffect;

    private GameObject effectsContainer;

    private AudioSource audioSource;
    public AudioClip impactSound;


    private void Start()
    {
        effectsContainer = GameObject.Find("Effects");
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
        {
            return;
        }

        // Update the score for every enemy hit
        foreach (Enemy enemy in GameManager.instance.spawnManager.enemies)
        {
            if (collision.gameObject.CompareTag(enemy.tag))
            {
                GameManager.instance.ChangeScore(enemy.value);
                GameManager.instance.UpdateUI();

                // Track whether any enemies were hit by the bullet
                hitEnemy = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instantiate an explosion effect at the point of impact with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(impactSound);
            GameObject effect  = (GameObject)Instantiate(explosionEffect, explosionPoint.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 1f);

            // If no enemies are hit, reset the combo multiplier 
            if (!hitEnemy)
            {
                GameManager.instance.combo = 0;
                OverlayUI.instance.UpdateComboText();
            }
            Destroy(gameObject);
        }
    }
}
