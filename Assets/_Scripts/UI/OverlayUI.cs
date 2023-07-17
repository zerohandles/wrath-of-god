using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class OverlayUI : MonoBehaviour
{
    public Slider wrathBar;
    public TextMeshProUGUI comboText;

    #region singleton
    public static OverlayUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    private void Start()
    {
        wrathBar.maxValue = GameManager.instance.PointsToWin;
        wrathBar.value = 0;
    }

    public void UpdateScore()
    {
        wrathBar.value = GameManager.instance.score;
    }

    public void UpdateComboText()
    {
        float combo = Mathf.Round(GameManager.instance.combo * 100);
        comboText.text = $"x{combo}";
    }
}
