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
    public bool isGameOver;


    // Start is called before the first frame update
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
        DontDestroyOnLoad(gameObject);

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

    public void Menu()
    {
        Debug.Log("Go to Menu");
    }

    public void LoadNextLevel()
    {
        Debug.Log("Load next level");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart level");
    }
}
