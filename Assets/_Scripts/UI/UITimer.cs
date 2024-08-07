using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [Header("Timer UI Elements")]
    [SerializeField] float m_TimeRemaining = 60;
    [SerializeField] TextMeshProUGUI timerText;
   
    public float TimeRemaining {get; private set; }

    void Awake() => TimeRemaining = m_TimeRemaining;

    // Update the timer every frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
        TimeRemaining = Mathf.Clamp(TimeRemaining, 0.0f, Mathf.Infinity);
        DisplayTime(TimeRemaining);
    }

    // Display the current time remaining in the level
    void DisplayTime(float timeInSeconds)
    {
        // Add 1 second to continue displaying 1 second when less than 1/2 second remains
        if (timeInSeconds > 0)
            timeInSeconds += 1;

        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
