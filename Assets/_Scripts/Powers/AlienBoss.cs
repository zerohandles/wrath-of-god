using System.Collections;
using UnityEngine;

public class AlienBoss : MonoBehaviour
{
    [Header("Ship FX")]
    [SerializeField] GameObject laser;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip explosionSound;

    Animator animator;
    Animator laserAnimator;
    Vector3 firingPos = new Vector3(0, 4.8f, 0);
    Vector3 startingPos;
    Vector3 targetPos;
    bool isInPosition = false;
    readonly float speed = 1f;


    void OnEnable()
    {
        targetPos = firingPos;
        audioSource.Stop();
    }

    void Start()
    {
        startingPos = transform.position;
        animator = GetComponent<Animator>();
        laserAnimator = laser.GetComponent<Animator>();
    }

    void Update()
    {
        if (isInPosition)
            return;

        // Move towards the target position and take action.
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            isInPosition = true;
            TakeAction();
        }

    }

    // Take action based on current target position
    void TakeAction()
    {
        if (targetPos == startingPos)
        {
            gameObject.SetActive(false);
            isInPosition = false;
        }
        else if (targetPos != startingPos)
            StartCoroutine(FireLaser());
    }

    // Play laser animation and SFX.
    IEnumerator FireLaser()
    {
        animator.SetTrigger("Fire");
        yield return new WaitForSeconds(.5f);
        audioSource.Play();
        yield return new WaitForSeconds(1.0f);
        audioSource.PlayOneShot(explosionSound);
        yield return new WaitForSeconds(1.0f);
        laserAnimator.SetTrigger("End");
        yield return new WaitForSeconds(.3f);
        audioSource.Stop();
        yield return new WaitForSeconds(1.7f);
        targetPos = startingPos;
        isInPosition = false;
    }
}
