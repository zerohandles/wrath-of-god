using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public GameObject menuUI;
    public TextMeshProUGUI menuTitleText;
    public TextMeshProUGUI menuDescriptionText;
    public TextMeshProUGUI nextButtonText;
    public GameObject menuBackground;
    public AudioSource audioSource;
    public AudioClip scrollSound;
    private readonly int secretLevelIndex = 5;
    private readonly string secretLevelName = "Level6";
    private readonly string finalLevelName = "Level5";

    // Called from GameOver in the Game Manager
    // Set menu text based on player's final score and enable UI elements
    public void SetGameOverText(bool victory)
    {
        if (!victory)
        {
            nextButtonText.text = "Restart";
            menuTitleText.text = "GAME OVER";
            menuDescriptionText.text = "You failed to smite the non-believers\nbefore they spread their blasphemy.\nYou are no longer feared.";
        }
        else if (victory)
        {
            nextButtonText.text = "Continue";
            menuTitleText.text = "Victory!";
            menuDescriptionText.text = $"You have smote {GameManager.instance.totalKilled} non-believers, \nreducing them to smoldering craters.\n The world fears you again.";
        }

        menuUI.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        menuBackground.SetActive(true);
        CheckSecretCondition();
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextButton()
    {
        if(GameManager.instance.score < GameManager.instance.PointsToWin)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            // Load a victory screen if player is on the last level
            if(SceneManager.GetActiveScene().name == finalLevelName)
            {
                // Load the secret level if player meet the condition
                if (CheckSecretCondition())
                {
                    SceneManager.LoadScene(secretLevelName);
                    return;
                }

                GoToVictoryScreen();
                return;
            }

            // Load true victory screen after beating the secret level
            if(SceneManager.GetActiveScene().name == secretLevelName)
            {
                GoToVictoryScreen();
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void GoToVictoryScreen()
    {
        if (SceneManager.GetActiveScene().name == finalLevelName)
        {
            SceneManager.LoadScene("VictoryScene");
        }
        else if (SceneManager.GetActiveScene().name == secretLevelName)
        {
            SceneManager.LoadScene("TrueVictoryScene");
        }
    }

    // Check player prefs if conditions for unlocking the final level have been met
    private bool CheckSecretCondition()
    {
        for (int i = 0; i < secretLevelIndex; i++)
        {
            if(PlayerPrefs.GetInt("level"+i) == 0)
            {
                return false;
            }
        }
        PlayerPrefs.SetInt("secretLevel", 1);
        PlayerPrefs.Save();
        return true;
    }
}
