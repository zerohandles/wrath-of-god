using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenu;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButton()
    {
        StartCoroutine(StartGame());
    }

    public void LevelSelectButton()
    {
        StartCoroutine(LevelSelect());
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

    private IEnumerator QuitGame()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        Application.Quit();
        Debug.Log("Quit");
    }
}
