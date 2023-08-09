using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    private GameObject target;
    private Vector3 targetPos;
    private Vector3 startingPos;
    private readonly float offset = -0.32f;
    private readonly float speed = 2f;
    private bool hasTarget = false;
    private bool abductedTarget = false;

    private Animator animator;
    public GameObject alienMotherShip;

    private FadeEffectAudio fader;

    private void OnEnable()
    {
        StartCoroutine(FindTarget());
    }

    void Awake()
    {
        startingPos = transform.position;
        animator = GetComponent<Animator>();
        fader = GetComponent<FadeEffectAudio>();
    }

    void Update()
    {
        var step = speed * Time.deltaTime;

        if(target != null)
        {
            targetPos = new Vector3(target.transform.position.x, offset, 0);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if(Vector3.Distance(transform.position, targetPos) < 0.0001f)
        {
            if (hasTarget)
            {
                StartCoroutine(AbductTarget());
                hasTarget = false;
                return;
            }

            if (abductedTarget)
            {
                alienMotherShip.SetActive(true);
                gameObject.SetActive(false);
                hasTarget = false;
                abductedTarget = false;
            }
        }
    }

    IEnumerator FindTarget()
    {
        if(GameObject.FindGameObjectWithTag("Cow"))
        {
            target = GameObject.FindGameObjectWithTag("Cow");
            foreach (Collider2D col in target.GetComponents<Collider2D>())
            {
                col.enabled = false;
            }
            hasTarget = true;
            StartCoroutine(fader.FadeAudio(.35f, 2));
        }
        else
        {
            targetPos = startingPos;
            yield return new WaitForSeconds(.2f);
            StartCoroutine(FindTarget());
        }
    }

    IEnumerator AbductTarget()
    {
        animator.SetTrigger("Above_target");
        yield return new WaitForSeconds(.5f);
        targetPos = startingPos;
        abductedTarget = true;
        StartCoroutine(fader.FadeAudio(0, 5));
        Destroy(target);
    }
}
