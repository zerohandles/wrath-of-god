using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject tutorialMenu;
    private Animator animator;

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
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private IEnumerator LevelSelect()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        Debug.Log("Load Level Selection scene");
    }

    private IEnumerator Tutorial()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        tutorialMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    private IEnumerator QuitGame()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        Application.Quit();
        Debug.Log("Quit");
    }
}
