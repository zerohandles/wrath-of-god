using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class OverlayUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Slider wrathBar;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] GameObject levelHeader;

    readonly float titleOnScreenTimer = 4f;

    #region singleton
    public static OverlayUI instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    void Start()
    {
        wrathBar.maxValue = GameManager.Instance.PointsToWin;
        wrathBar.value = 0;
        StartCoroutine(DisplayTitleBanner());
    }

    IEnumerator DisplayTitleBanner()
    {
        levelHeader.SetActive(true);
        yield return new WaitForSeconds(titleOnScreenTimer);
        levelHeader.SetActive(false);
    }

    public void UpdateScore() => wrathBar.value = GameManager.Instance.score;

    public void UpdateComboText()
    {
        float combo = Mathf.Round(GameManager.Instance.combo * 100);
        comboText.text = $"x{combo}";
    }
}
