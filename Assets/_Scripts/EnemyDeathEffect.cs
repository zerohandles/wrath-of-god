using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEffect : MonoBehaviour
{
    public GameObject deathEffect;
    private GameObject effectsContainer;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        effectsContainer = GameObject.Find("Effects");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Lightning"))
        {
            rend.enabled = false;
            GameObject effect = (GameObject)Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 5f);
            Destroy(gameObject);
        }
    }
}
