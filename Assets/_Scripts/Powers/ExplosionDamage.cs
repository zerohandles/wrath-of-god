using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.GetComponent<EnemyMovement>())
            return;

        // Score each enemy that collides with the explosion
        foreach (Enemy enemy in GameManager.Instance.spawnManager.enemies)
        {
            if (collision.gameObject.CompareTag(enemy.tag))
            {
                GameManager.Instance.ChangeScore(enemy.value);
                GameManager.Instance.UpdateUI();
            }
        }
    }
}
