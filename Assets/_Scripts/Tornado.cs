using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] float lifetime = 7f;
    private float timer;
    private FadeEffectAudio fader;
    private float fadeDuration = 1;


    private void Start()
    {
        fader = GetComponent<FadeEffectAudio>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > lifetime)
        {
            StartCoroutine(fader.FadeAudio(0, fadeDuration));
            DisableAllColliders();
            Destroy(gameObject, fadeDuration);
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
                GameManager.instance.UpdateUI();
            }
        }
    }
}
