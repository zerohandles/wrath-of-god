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

        transform.Translate(speed * Time.deltaTime * Vector2.down, Space.Self);
    }
}
