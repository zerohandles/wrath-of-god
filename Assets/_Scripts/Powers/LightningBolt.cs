using UnityEngine;
using UnityEngine.Pool;

public class LightningBolt : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] GameObject explosionPoint;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] AudioClip impactSound;
    GameObject effectsContainer;
    AudioSource audioSource;
    bool hitEnemy = false;

    [Header("Movement")]
    [SerializeField] float movementSpeed;
    readonly float lifeTime = 5f;
    float timer;

    ObjectPool<LightningBolt> _pool;

    private void OnEnable()
    {
        timer = 0;
        hitEnemy = false;
    }

    private void Start()
    {
        effectsContainer = GameObject.Find("Effects");
        audioSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
    }

    void Update()
    {
        // Destroy stray bullets that don't impact the ground after a set amount of time
        timer += Time.deltaTime;
        if (timer > lifeTime)
        {
            //Destroy(gameObject);
            _pool.Release(this);
        }

        transform.Translate(movementSpeed * Time.deltaTime * Vector2.down, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
            return;

        // Update the score for every enemy hit
        foreach (Enemy enemy in GameManager.Instance.spawnManager.enemies)
        {
            if (collision.gameObject.CompareTag(enemy.tag))
            {
                GameManager.Instance.ChangeScore(enemy.value);
                GameManager.Instance.UpdateUI();

                // Track whether any enemies were hit by the bullet
                hitEnemy = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Instantiate an explosion effect at the point of impact with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioSource.PlayOneShot(impactSound);
            GameObject effect  = (GameObject)Instantiate(explosionEffect, explosionPoint.transform.position, Quaternion.identity);
            effect.transform.SetParent(effectsContainer.transform);
            Destroy(effect, 1f);

            // If no enemies are hit, reset the combo multiplier 
            if (!hitEnemy)
            {
                GameManager.Instance.combo = 0;
                OverlayUI.instance.UpdateComboText();
            }
            // Destroy(gameObject);
            if(gameObject.activeSelf)
                _pool.Release(this);
        }
    }

    public void SetPool(ObjectPool<LightningBolt> pool) => _pool = pool;
}
