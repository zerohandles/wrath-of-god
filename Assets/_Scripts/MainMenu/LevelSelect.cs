using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelect : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] GameObject[] mercyIcon;
    [SerializeField] Button[] levelButtons;
    [SerializeField] Button secretLevelButton;
    [SerializeField] TextMeshProUGUI secretLevelText;

    Animator animator;
    SecondaryMenu menu;

    void Start()
    {
        animator = GetComponent<Animator>();
        menu = GetComponent<SecondaryMenu>();

        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        // Set each level button beyond the levelReached player pref to non-interacttable
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
            // Enable icon if secret condition for that level was met
            if (PlayerPrefs.GetInt("level"+i) == 1)
                mercyIcon[i].SetActive(true);
        }

        // If unlocked make the secret level button interactable
        if (PlayerPrefs.GetInt("secretLevel") == 1)
        {
            secretLevelText.text = "Final Day - Armageddon";
            secretLevelButton.interactable = true;
        }
    }

    public void SelectLevel(int levelIndex) => StartCoroutine(LoadLevel(levelIndex));

    // Load the selected level
    IEnumerator LoadLevel(int levelIndex)
    {
        animator.SetTrigger("Close");
        menu.PlaySFX();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndex);
    }
}
