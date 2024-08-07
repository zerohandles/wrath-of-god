using System.Collections;
using UnityEngine;

public class Alien : MonoBehaviour
{
    GameObject target;
    Animator animator;
    FadeEffectAudio fader;
    Vector3 targetPos;
    Vector3 startingPos;
    readonly float offset = -0.32f;
    readonly float speed = 2f;
    bool hasTarget = false;
    bool abductedTarget = false;

    public GameObject alienMotherShip;


    private void OnEnable() => StartCoroutine(FindTarget());

    void Awake()
    {
        startingPos = transform.position;
        animator = GetComponent<Animator>();
        fader = GetComponent<FadeEffectAudio>();
    }

    void Update()
    {
        var step = speed * Time.deltaTime;

        // Update the target's position every frame to follow moving targets
        if(target != null)
            targetPos = new Vector3(target.transform.position.x, offset, 0);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        // Executed when at the target position
        if(Vector3.Distance(transform.position, targetPos) < 0.0001f)
        {
            if (hasTarget)
            {
                StartCoroutine(AbductTarget());
                hasTarget = false;
                return;
            }

            // Enable the mothership and reset varibles after returning to the starting point
            if (abductedTarget)
            {
                alienMotherShip.SetActive(true);
                gameObject.SetActive(false);
                hasTarget = false;
                abductedTarget = false;
            }
        }
    }


    // Repeatedly search for a valid target every .2 seconds until one is found.
    IEnumerator FindTarget()
    {
        if(GameObject.FindGameObjectWithTag("Cow"))
        {
            target = GameObject.FindGameObjectWithTag("Cow");

            // Disable the target's colliders to avoid the target being destroyed before
                //animations and coroutine's finish executing 
            foreach (Collider2D col in target.GetComponents<Collider2D>())
                col.enabled = false;
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

    // Play abduction animation and return to starting point
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
