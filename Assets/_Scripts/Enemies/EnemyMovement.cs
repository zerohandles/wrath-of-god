using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float maxSpeed = 1.5f;
    [SerializeField] float minSpeed = .35f;
    [SerializeField] float stopTimerMax = 8f;
    [SerializeField] float stopTimerMin = 4f;
    [SerializeField] float moveTimerMax = 8f;
    [SerializeField] float moveTimerMin = 4f;

    [Header("Flood Effects")]
    [SerializeField] float floodSpeed = 5;

    Animator animator;
    Rigidbody2D rb;
    float speed;
    float moveTimer;
    readonly float floodWall = -8;
    bool isStopped = false;
    bool isInFlood = false;

    // Reset movement parameters and animation after retrieving from the enemy pool
    void OnEnable()
    {
        SetRandomSpeed();
        SetMoveTimer();
        animator.SetFloat("speed", 1);
        isStopped = false;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //  Move enemy towards the floodwall when the flood is active
        if (isInFlood && !isStopped)
        {
            var step = floodSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(floodWall, transform.position.y), step);
            if (Vector3.Distance(transform.position, new Vector3(floodWall, transform.position.y)) < 0.001f)
                isStopped = true;

            return;
        }

        if (isStopped)
            return;

        // Stop movement after the move timer reaches 0
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            isStopped = true;
            animator.SetFloat("speed", 0);
            StartCoroutine(StopMovementTimer());
        }

        transform.Translate(speed * Time.deltaTime * Vector2.right);
    }

    // After a certain amount of time, reset movement parameters and animation
    IEnumerator StopMovementTimer()
    {
        yield return new WaitForSeconds(Random.Range(stopTimerMin, stopTimerMax));
        SetRandomSpeed();
        SetMoveTimer();
        isStopped = false;
        animator.SetFloat("speed", 1);
    }

    // Called on enable and everytime enemy starts moving.
    void SetRandomSpeed() => speed = Random.Range(minSpeed, maxSpeed);

    // Called on enable and everytime enemy starts moving.
    void SetMoveTimer() => moveTimer = Random.Range(moveTimerMin, moveTimerMax);

    // After exiting a tornado's effect radius, reset velocity to zero to prevent enemy from flying off the screen
    void EndTornadoEffect()
    {
        rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    // Reset enemy's velocity after the flood ends to continue normal movement/animation
    void EndFloodEffect()
    {
        rb.velocity = Vector3.zero;
        isInFlood = false;
        isStopped = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Flood"))
        {
            isStopped = false;
            isInFlood = true;
        }
    }

    // End the effects of a power upon exiting its trigger collider
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Tornado"))
            EndTornadoEffect();

        if (collision.transform.CompareTag("Flood"))
            EndFloodEffect();
    }
}
