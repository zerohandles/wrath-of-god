using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Innocent : MonoBehaviour
{
    private Animator animator;
    private EnemyMovement movement;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<EnemyMovement>();
    }

    public void PrepareForRescue()
    {
        animator.SetFloat("speed", 0f);
        movement.enabled = false;

        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
    }
}
