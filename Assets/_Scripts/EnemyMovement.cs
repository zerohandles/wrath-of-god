using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed;
    private float moveTimer;
    private bool isStopped = false;
    
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private float maxSpeed = 1.5f;
    [SerializeField] private float minSpeed = .35f;
    [SerializeField] private float stopTimerMax = 8f;
    [SerializeField] private float stopTimerMin = 4f;
    [SerializeField] private float moveTimerMax = 8f;
    [SerializeField] private float moveTimerMin = 4f;

    [SerializeField] private float floodSpeed = 5;
    private readonly float floodWall = -8;
    private bool isInFlood = false;

    private void OnEnable()
    {
        SetRandomSpeed();
        SetMoveTimer();
        animator.SetFloat("speed", 1);
        isStopped = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isInFlood && !isStopped)
        {
            var step = floodSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(floodWall, transform.position.y), step);
            if (Vector3.Distance(transform.position, new Vector3(floodWall, transform.position.y)) < 0.001f)
            {
                isStopped = true;
            }

            return;
        }

        if (isStopped)
        {
            return;
        }

        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            isStopped = true;
            animator.SetFloat("speed", 0);
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
        animator.SetFloat("speed", 1);
    }

    private void SetRandomSpeed()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void SetMoveTimer()
    {
        moveTimer = Random.Range(moveTimerMin, moveTimerMax);
    }

    void EndTornadoEffect()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    void EndFloodEffect()
    {
        rb.velocity = Vector3.zero;
        isInFlood = false;
        isStopped = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Flood"))
        {
            isStopped = false;
            isInFlood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Tornado"))
        {
            EndTornadoEffect();
        }

        if (collision.transform.CompareTag("Flood"))
        {
            EndFloodEffect();
        }
    }
}
