using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private Animator animator;
    private Vector3 target;
    private readonly float targetXRange = 8.3f;
    private readonly float targetY = -2;

    [SerializeField] private float speed;
    private bool isMoving = true;

    public AudioSource audioSource;
    public AudioClip impactSound;


    // Set a random target position on screen. Rotate to match angle of target position
    private void OnEnable()
    {
        target = new Vector3(Random.Range(-targetXRange, targetXRange), targetY, 0);
        Vector3 dir = (target - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector3.right, dir);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            return;
        }

        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
    }

    // Destroy meteor when it hits the ground and play impact animation/SFX
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            transform.rotation = Quaternion.identity;
            audioSource.PlayOneShot(impactSound);
            animator.SetTrigger("Impact");
            isMoving = false;
            Destroy(gameObject,1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
        {
            return;
        }

        // Score any enemies hit
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
