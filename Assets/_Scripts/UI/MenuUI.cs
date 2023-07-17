using System.Collections;
using System.Collections.Generic;
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
            menuDescriptionText.text = "You have smote the non-believers, \nreducing them to smoldering craters.\n The world fears you again.";
        }

        menuUI.SetActive(true);
        menuBackground.SetActive(true);
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
            Debug.Log("Load next level");
        }
    }
}
