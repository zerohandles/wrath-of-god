using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    public GameObject deathEffect;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Lightning"))
        {
            rend.enabled = false;
            GameObject effect = (GameObject)Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
}
