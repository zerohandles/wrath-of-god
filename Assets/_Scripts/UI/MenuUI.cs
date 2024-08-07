using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject menuUI;
    [SerializeField] TextMeshProUGUI menuTitleText;
    [SerializeField] TextMeshProUGUI menuDescriptionText;
    [SerializeField] TextMeshProUGUI nextButtonText;
    [SerializeField] GameObject menuBackground;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip scrollSound;

    readonly int secretLevelIndex = 5;
    readonly string secretLevelName = "Level6";
    readonly string finalLevelName = "Level5";

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
            menuDescriptionText.text = $"You have smote {GameManager.Instance.totalKilled} non-believers, \nreducing them to smoldering craters.\n The world fears you again.";
        }

        menuUI.SetActive(true);
        audioSource.PlayOneShot(scrollSound);
        menuBackground.SetActive(true);
        CheckSecretCondition();
    }

    public void MenuButton() => SceneManager.LoadScene("MainMenu");

    // Determine what the continue button will do when the level has ended
    public void NextButton()
    {
        // Retry same level if player didn't get a high enough score
        if(GameManager.Instance.score < GameManager.Instance.PointsToWin)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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

            // Load the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Loads the game's victory scene
    public void GoToVictoryScreen()
    {
        if (SceneManager.GetActiveScene().name == finalLevelName)
            SceneManager.LoadScene("VictoryScene");
        else if (SceneManager.GetActiveScene().name == secretLevelName)
            SceneManager.LoadScene("TrueVictoryScene");
    }

    // Check player prefs if conditions for unlocking the final level have been met
    bool CheckSecretCondition()
    {
        for (int i = 0; i < secretLevelIndex; i++)
        {
            if(PlayerPrefs.GetInt("level"+i) == 0)
                return false;
        }
        PlayerPrefs.SetInt("secretLevel", 1);
        PlayerPrefs.Save();
        return true;
    }
}
