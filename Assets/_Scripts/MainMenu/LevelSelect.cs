using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    private Animator animator;
    private SecondaryMenu menu;
    [SerializeField] GameObject[] mercyIcon;
    [SerializeField] Button[] levelButtons;
    [SerializeField] Button secretLevelButton;
    [SerializeField] TextMeshProUGUI secretLevelText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        menu = GetComponent<SecondaryMenu>();

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            if (PlayerPrefs.GetInt("level"+i) == 1)
            {
                mercyIcon[i].SetActive(true);
            }
        }

        if (PlayerPrefs.GetInt("secretLevel") == 1)
        {
            secretLevelText.text = "Final Day - Armageddon";
            secretLevelButton.interactable = true;
        }
    }

    public void SelectLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Close");
        menu.PlaySFX();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
