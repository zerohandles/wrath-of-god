using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject tutorialMenu;
    public GameObject levelSelectMenu;
    public GameObject tutorialLevelPrompt;
    private Animator animator;
    public AudioClip scrollSound;
    public AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartGameButton()
    {
        tutorialLevelPrompt.SetActive(true);
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

    public void TutorialPromptYes()
    {
        StartCoroutine(StartTutorial());
    }

    public void TutorialPromptNo()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        tutorialLevelPrompt.SetActive(false);
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private IEnumerator StartTutorial()
    {
        tutorialLevelPrompt.SetActive(false);
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TutorialLevel");
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
