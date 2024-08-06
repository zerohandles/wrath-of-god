using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager spawnManager;
    private MenuUI menuUI;
    private UITimer timer;
    public AudioSource sFXSource;

    [Header("Units")]
    [SerializeField] int totalInnocentsInLevel;
    [HideInInspector] public int innocentsKilled = 0;
    [HideInInspector] public int totalKilled = 0;

    [Header("Score and Combo")]
    [SerializeField] private float m_pointsToWin;
    public float PointsToWin { get; private set; }
    public float score;
    [HideInInspector] public float combo = 0;
    [SerializeField] private float comboTimeLimit = 1.5f;
    [SerializeField] private float comboTimer = 0;

    [Header("Victory Player Pref Data")]
    [SerializeField] private int nextLevelNumber;
    [SerializeField] private int currentLevelIndex;
    [HideInInspector] public bool isGameOver;

    void Awake()
    {
    #region "Singleton"
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        #endregion

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
        // Trigger game over if time runs out
        if (timer.TimeRemaining <= 0.0f && !isGameOver)
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

    // Set game over and check if player won or lost the level
    void GameOver()
    {
        isGameOver = true;
        
        // Had to stop SFX to avoid last SFX getting stuck playing in a loop 
        sFXSource.Stop();

        // If player lost, set game over text and UI elements
        if(score < PointsToWin)
        {
            menuUI.SetGameOverText(false);
        }

        // If player won set victory text/UI element. Update player prefs with level complete
        else if (score >= PointsToWin)
        {
            menuUI.SetGameOverText(true);
            if (PlayerPrefs.GetInt("levelReached") < nextLevelNumber)
            {
                PlayerPrefs.SetInt("levelReached", nextLevelNumber);
            }

            // Condition for unlocking the secret level
            if (innocentsKilled == totalInnocentsInLevel)
            {
                PlayerPrefs.SetInt("level" + currentLevelIndex, 1);
            }

            PlayerPrefs.Save();
        }
    }

    // Update the player's score and kill count
    public void ChangeScore(float value)
    {
        score += value * (1 + combo);
        totalKilled += 1;
    }

    // Update the score keeping UI elements
    public void UpdateUI()
    {
        combo += .01f;
        comboTimer = 0;
        OverlayUI.instance.UpdateScore();
        OverlayUI.instance.UpdateComboText();
    }

    // Score innocent enemies when they are rescued rather than killed.
    public void ScoreInnocent(GameObject target)
    {
        foreach (Enemy enemy in spawnManager.enemies)
        {
            if (target.CompareTag(enemy.tag))
            {
                ChangeScore(-enemy.value);
                UpdateUI();
            }
        }
    }
}
