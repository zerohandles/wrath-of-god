using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    private Animator animator;
    public GameObject mainMenu;
    public GameObject tutorialMenu;

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
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(true);
        tutorialMenu.SetActive(false);
    }
}
