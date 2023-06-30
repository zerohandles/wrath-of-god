using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    private float lifeTime = 3f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        // Add object pooling
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Explode");

            // Use object pooling for lightning bolts
            Destroy(gameObject);
        }
    }
}
