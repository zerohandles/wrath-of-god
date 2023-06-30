using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed;
    private float moveTimer;
    private float stopTimer;
    private bool isStopped = false;

    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float minSpeed = .35f;
    [SerializeField] private float stopTimerMax = 8f;
    [SerializeField] private float stopTimerMin = 4f;

    private void Start()
    {
        stopTimer = stopTimerMin;
        InvokeRepeating(nameof(SetSpeed), 1, 5);
    }

    void Update()
    {
        stopTimer -= Time.deltaTime;
        if (stopTimer <= 0)
        {
            isStopped = true;
        }

        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }


    void SetSpeed()
    {
        if (isStopped)
        {
            speed = 0;
            stopTimer = Random.Range(stopTimerMin, stopTimerMax);
            isStopped = false;
        }
        else
        {
            speed = Random.Range(minSpeed, maxSpeed);
        }
    }

}
