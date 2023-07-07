using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class OverlayUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

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

    public void UpdateScore()
    {
        scoreText.text = $"Score: {GameManager.Instance.score}";
    }
}
