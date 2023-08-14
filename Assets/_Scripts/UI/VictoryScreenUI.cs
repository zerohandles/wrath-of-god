using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenUI : MonoBehaviour
{
    // Load the main menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
