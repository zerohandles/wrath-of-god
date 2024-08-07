using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject tutorialMenu;
    [SerializeField] GameObject levelSelectMenu;
    [SerializeField] GameObject tutorialLevelPrompt;
    [SerializeField] AudioClip scrollSound;
    [SerializeField] AudioSource audioSource;
    
    Animator animator;

    void Start() => animator = GetComponent<Animator>();

    public void StartGameButton() => tutorialLevelPrompt.SetActive(true);

    public void LevelSelectButton() => StartCoroutine(LevelSelect());

    public void TutorialButton() => StartCoroutine(Tutorial());

    public void QuitButton() => StartCoroutine(QuitGame());

    public void TutorialPromptYes() => StartCoroutine(StartTutorial());

    public void TutorialPromptNo() => StartCoroutine(StartGame());

    IEnumerator CloseScroll()
    {
        animator.SetTrigger("Close");
        audioSource.PlayOneShot(scrollSound);
        yield return null;
    }

    private IEnumerator StartGame()
    {
        tutorialLevelPrompt.SetActive(false);
        StartCoroutine(CloseScroll());
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }

    private IEnumerator StartTutorial()
    {
        tutorialLevelPrompt.SetActive(false);

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("TutorialLevel");
    }

    private IEnumerator LevelSelect()
    {
        StartCoroutine(CloseScroll());
        yield return new WaitForSeconds(1);
        levelSelectMenu.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        mainMenu.SetActive(false);
    }

    private IEnumerator Tutorial()
    {
        StartCoroutine(CloseScroll());
        yield return new WaitForSeconds(1);
        tutorialMenu.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        mainMenu.SetActive(false);
    }

    private IEnumerator QuitGame()
    {
        StartCoroutine(CloseScroll());
        yield return new WaitForSeconds(1);
        Application.Quit();
        Debug.Log("Quit");
    }
}
