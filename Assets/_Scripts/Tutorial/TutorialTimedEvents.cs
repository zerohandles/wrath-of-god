using System.Collections;
using UnityEngine;

// Scripted events for the tutorial scene
public class TutorialTimedEvents : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] TutorialPlayer player;
    
    [Header("UI Elements")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject menuBackground;
    [SerializeField] GameObject fog;
    [SerializeField] GameObject spriteMask1;

    [Header("Event 1")]
    [SerializeField] GameObject eventText1;
    bool event1Complete = false;

    [Header("Event 2")]
    [SerializeField] GameObject eventText2;
    [SerializeField] GameObject combo;
    bool event2Complete = false;

    [Header("Event 3")]
    [SerializeField] GameObject eventText3;
    [SerializeField] GameObject wrathMask;
    [SerializeField] GameObject timerMask;
    bool event3Complete = false;

    [Header("Event 4")]
    [SerializeField] GameObject eventText4;
    [SerializeField] GameObject innocentPrefab;
    bool event4Complete = false;

    [Header("Event 5")]
    [SerializeField] GameObject eventText5;
    [SerializeField] GameObject powersMask;
    [SerializeField] GameObject tornadoButton;
    bool event5Complete = false;

    bool tutorialCompleted = false;

    void Start()
    {
        player.canShoot = false;
        StartCoroutine(FirstEvent());
    }


    void Update()
    {
        if (event1Complete && GameManager.Instance.score > 0 && !event2Complete)
        {
            event2Complete = true;
            StartCoroutine(SecondEvent());
        }

        if (event2Complete && GameManager.Instance.score > 300 && !event3Complete)
        {
            event3Complete = true;
            StartCoroutine(ThirdEvent());
        }

        if (event3Complete && GameManager.Instance.score >= 400 && !event4Complete)
        {
            event4Complete = true;
            StartCoroutine(FourthEvent());
        }

        if(event4Complete && GameManager.Instance.score >= 2000 && !event5Complete)
        {
            event5Complete = true;
            StartCoroutine(FifthEvent());
        }

        if (tutorialCompleted)
        {
            menuBackground.gameObject.SetActive(true);
            menuCanvas.gameObject.SetActive(true);
        }
    }

    IEnumerator FirstEvent()
    {
        yield return new WaitForSeconds(5);
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy1");
        Time.timeScale = 0;
        fog.SetActive(true);
        spriteMask1.transform.position = enemy.transform.position;
        spriteMask1.SetActive(true);
        eventText1.SetActive(true);
        yield return new WaitForSecondsRealtime(5);

        spriteMask1.SetActive(false);
        eventText1.SetActive(false);
        fog.SetActive(false);
        Time.timeScale = 1;
        player.canShoot = true;
        event1Complete = true;
    }

    IEnumerator SecondEvent()
    {
        yield return new WaitForSeconds(.5f);
        player.canShoot = false;
        spriteMask1.transform.position = combo.transform.position;
        fog.SetActive(true);
        spriteMask1.SetActive(true);
        eventText2.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(10);

        fog.SetActive(false);
        spriteMask1.SetActive(false);
        eventText2.SetActive(false);
        Time.timeScale = 1;
        player.canShoot = true;
    }

    IEnumerator ThirdEvent()
    {
        yield return new WaitForSeconds(.5f);
        player.canShoot = false;
        fog.SetActive(true);
        wrathMask.SetActive(true);
        timerMask.SetActive(true);
        eventText3.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(10);

        fog.SetActive(false);
        wrathMask.SetActive(false);
        timerMask.SetActive(false);
        eventText3.SetActive(false);
        Time.timeScale = 1;
        player.canShoot = true;
    }

    IEnumerator FourthEvent()
    {
        player.canShoot = false;
        GameObject innocent = Instantiate(innocentPrefab, new Vector3(-9.5f, -2.4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(3);

        Time.timeScale = 0;
        spriteMask1.transform.position = innocent.transform.position;
        fog.SetActive(true);
        spriteMask1.SetActive(true);
        eventText4.SetActive(true);
        yield return new WaitForSecondsRealtime(12);

        spriteMask1.SetActive(false);
        eventText4.SetActive(false);
        fog.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator FifthEvent()
    {
        yield return new WaitForSeconds(4);
        fog.SetActive(true);
        powersMask.SetActive(true);
        eventText5.SetActive(true);
        tornadoButton.SetActive(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(6);

        fog.SetActive(false);
        powersMask.SetActive(false);
        eventText5.SetActive(false);
        Time.timeScale = 1;
        yield return new WaitForSeconds(2);
        tutorialCompleted = true;
    }
}
