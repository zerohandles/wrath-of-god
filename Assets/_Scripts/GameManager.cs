using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager spawnManager;
    public GameObject menuUI;
    public TextMeshProUGUI menuTitleText;
    public TextMeshProUGUI menuDescriptionText;
    public GameObject menuBackground;
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
        menuUI.SetActive(true);
        menuBackground.SetActive(true);

        if(score < PointsToWin)
        {
            menuTitleText.text = "GAME OVER";
            menuDescriptionText.text = "You failed to smite the non-believers\nbefore they spread their blasphemy.\nYou are no longer feared.";
        }
        else if (score >= PointsToWin)
        {
            menuTitleText.text = "Victory!";
            menuDescriptionText.text = "You have smote the non-believes \nreducing them to smoking craters.\n The world fears you again.";
        }
    }
}
