using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    private Animator animator;
    public GameObject laser;
    private Animator laserAnimator;

    private bool isInPosition = false;
    private Vector3 firingPos = new Vector3(0, 4.8f, 0);
    private Vector3 startingPos;
    private Vector3 targetPos;

    private float speed = 1f;


    private void OnEnable()
    {
        targetPos = firingPos;
    }

    private void Start()
    {
        startingPos = transform.position;
        animator = GetComponent<Animator>();
        laserAnimator = laser.GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInPosition)
        {
            return;
        }

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            isInPosition = true;
            TakeAction();
        }

    }

    void TakeAction()
    {
        if (targetPos == startingPos)
        {
            gameObject.SetActive(false);
            isInPosition = false;
        }
        else if (targetPos != startingPos)
        {
            StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {
        animator.SetTrigger("Fire");
        yield return new WaitForSeconds(2.5f);
        laserAnimator.SetTrigger("End");
        yield return new WaitForSeconds(2);
        targetPos = startingPos;
        isInPosition = false;
    }
}
