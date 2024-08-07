using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float speed;
    
    ObjectPool<Meteor> _pool;
    Animator animator;
    Vector3 target;
    readonly float targetXRange = 8.3f;
    readonly float targetY = -2.3f;
    bool isMoving = true;
    public AudioSource audioSource;
    public AudioClip impactSound;


    // Set a random target position on screen. Rotate to match angle of target position
    void OnEnable()
    {
        isMoving = true;
        target = new Vector3(Random.Range(-targetXRange, targetXRange), targetY, 0);
        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start() => animator = GetComponent<Animator>();

    private void Update()
    {
        if (!isMoving)
            return;

        // Triggers expplosion when meteor is near target even if it doesn't collide with the ground
        if (Vector3.Distance(target, transform.position) <= 0.1f)
            StartCoroutine(TriggerExplosion());

        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator TriggerExplosion()
    {
        isMoving = false;
        animator.SetTrigger("Impact");
        transform.rotation = Quaternion.identity;
        audioSource.PlayOneShot(impactSound);
        yield return new WaitForSeconds(1);
        _pool.Release(this);
    }

    // Destroy meteor when it hits the ground and play impact animation/SFX
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(TriggerExplosion());
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
            return;

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
    public void SetPool(ObjectPool<Meteor> pool) => _pool = pool;
}
