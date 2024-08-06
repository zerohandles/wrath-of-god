using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private readonly float lifeTime = 5f;
    private float timer;

    void Update()
    {
        // Destroy stray bullets that don't impact the ground after a set amount of time
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector2.down, Space.Self);
    }
}
