using UnityEngine;

public class Tornado : MonoBehaviour
{
    [SerializeField] float lifetime = 7f;
    private float timer;
    private FadeEffectAudio fader;
    private readonly float fadeDuration = 1;


    private void Start()
    {
        fader = GetComponent<FadeEffectAudio>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // After its lifetime disable the tornado's effects and destroy it
        if(timer > lifetime)
        {
            StartCoroutine(fader.FadeAudio(0, fadeDuration));
            DisableAllColliders();
            Destroy(gameObject, fadeDuration);
        }
    }

    // Turn off all colliders to stop enemies from being sucked in
    void DisableAllColliders()
    {
        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
    }

    // Score any enemy sucked into the tornado
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
        {
            return;
        }

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
