using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed;
    private float moveTimer;
    private bool isStopped = false;

    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float minSpeed = .35f;
    [SerializeField] private float stopTimerMax = 8f;
    [SerializeField] private float stopTimerMin = 4f;
    [SerializeField] private float moveTimerMax = 8f;
    [SerializeField] private float moveTimerMin = 4f;

    private void Awake()
    {
        SetRandomSpeed();
        SetMoveTimer();
    }

    void Update()
    {
        if (isStopped)
        {
            return;
        }

        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            isStopped = true;
            StartCoroutine(StopMovementTimer());
        }

        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    IEnumerator StopMovementTimer()
    {
        yield return new WaitForSeconds(Random.Range(stopTimerMin, stopTimerMax));
        SetRandomSpeed();
        SetMoveTimer();
        isStopped = false;
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void SetMoveTimer()
    {
        moveTimer = Random.Range(moveTimerMin, moveTimerMax);
    }

}
