using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            menuTitleText.text = "GAME OVER";
            menuDescriptionText.text = "You failed to smite the non-believers\nbefore they spread their blasphemy.\nYou are no longer feared.";
        }
        else if (victory)
        {
            menuTitleText.text = "Victory!";
            menuDescriptionText.text = "You have smote the non-believers, \nreducing them to smoldering craters.\n The world fears you again.";
        }

        menuUI.SetActive(true);
        menuBackground.SetActive(true);
    }

    public void MenuButton()
    {
        GameManager.instance.Menu();
    }

    public void NextButton()
    {
        if(GameManager.instance.score < GameManager.instance.PointsToWin)
        {
            GameManager.instance.RestartLevel();
            nextButtonText.text = "Restart";
        }
        else
        {
            GameManager.instance.LoadNextLevel();
            nextButtonText.text = "Continue";
        }
    }
}
