using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject tutorialMenu;
    public GameObject levelSelectMenu;
    private Animator animator;
    public AudioClip scrollSound;
    public AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartGameButton()
    {
        StartCoroutine(StartGame());
    }

    public void LevelSelectButton()
    {
        StartCoroutine(LevelSelect());
    }

    public void TutorialButton()
    {
        StartCoroutine(Tutorial());
    }

    public void QuitButton()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator StartGame()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private IEnumerator LevelSelect()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        levelSelectMenu.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        mainMenu.SetActive(false);
    }

    private IEnumerator Tutorial()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        tutorialMenu.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        mainMenu.SetActive(false);
    }

    private IEnumerator QuitGame()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        Application.Quit();
        Debug.Log("Quit");
    }
}
