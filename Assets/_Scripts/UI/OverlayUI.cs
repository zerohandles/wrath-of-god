using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class OverlayUI : MonoBehaviour
{
    public Slider wrathBar;

    #region singleton
    public static OverlayUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
}
