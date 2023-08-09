using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    private Animator animator;
    public GameObject mainMenu;
    public GameObject tutorialMenu;
    public AudioSource audioSource;
    public AudioClip scrollSound;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BackButton()
    {
        StartCoroutine(Back());
    }

    private IEnumerator Back()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        tutorialMenu.SetActive(false);
    }
}
