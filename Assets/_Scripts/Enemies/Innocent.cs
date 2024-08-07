using UnityEngine;

public class Innocent : MonoBehaviour
{
    Animator animator;
    EnemyMovement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    // Called when an innocent is being rescued
    public void PrepareForRescue()
    {
        // Stops movement and animation
        animator.SetFloat("speed", 0f);
        movement.enabled = false;

        // Disable colliders to prevent the innocent from dying in mid rescue animation
        foreach (Collider2D col in GetComponents<Collider2D>())
            col.enabled = false;
    }
}
