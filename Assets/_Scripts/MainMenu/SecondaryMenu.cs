using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryMenu : MonoBehaviour
{
    private Animator animator;
    public GameObject mainMenu;
    public GameObject secondaryMenu;
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
        PlaySFX();
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(true);
        PlaySFX();
        secondaryMenu.SetActive(false);
    }

    public void PlaySFX()
    {
        audioSource.PlayOneShot(scrollSound);
    }
}
