using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField] private float timeRemaining = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        timeRemaining = Mathf.Clamp(timeRemaining, 0.0f, Mathf.Infinity);
        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeInSeconds)
    {
        // Add 1 second to continue displaying 1 second when less than 1/2 second remains
        if (timeInSeconds > 0)
        {
            timeInSeconds += 1;
        }

        float minutes = Mathf.FloorToInt(timeInSeconds / 60);
        float seconds = Mathf.FloorToInt(timeInSeconds % 60);

        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
