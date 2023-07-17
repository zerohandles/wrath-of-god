using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager spawnManager;
    private MenuUI menuUI;
    private UITimer timer;

    public float PointsToWin { get; private set; }
    [SerializeField] private float m_pointsToWin;
    public float score;
    public float combo = 0;
    [SerializeField] private float comboTimeLimit = 1.5f;
    public float comboTimer = 0;

    public bool isGameOver;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
       // DontDestroyOnLoad(gameObject);

        spawnManager = gameObject.GetComponent<SpawnManager>();
        menuUI = gameObject.GetComponent<MenuUI>();
        PointsToWin = m_pointsToWin;
        isGameOver = false;
    }

    private void Start()
    {
        timer = GetComponent<UITimer>();
    }


    private void Update()
    {
        if (timer.TimeRemaining <= 0.0f)
        {
            GameOver();
        }

        // Reset combo timer if new enemies aren't killed fast enough
        comboTimer += Time.deltaTime;
        if (comboTimer > comboTimeLimit)
        {
            combo = 0;
            OverlayUI.instance.UpdateComboText();
        }
    }


    void GameOver()
    {
        isGameOver = true;

        if(score < PointsToWin)
        {
            menuUI.SetGameOverText(false);
        }
        else if (score >= PointsToWin)
        {
            menuUI.SetGameOverText(true);
        }
    }

    public void ChangeScore(Enemy enemy)
    {
        score += enemy.value * (1 + combo);
    }
}
