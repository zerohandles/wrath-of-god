using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Used during testing to modify player prefs and level unlocking
public class ClearPlayerPrefs : MonoBehaviour
{
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UnlockEverything()
    {
        PlayerPrefs.SetInt("levelReached", 6);
        PlayerPrefs.SetInt("level0", 1);
        PlayerPrefs.SetInt("level1", 1);
        PlayerPrefs.SetInt("level2", 1);
        PlayerPrefs.SetInt("level3", 1);
        PlayerPrefs.SetInt("level4", 1);
        PlayerPrefs.SetInt("secretLevel", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
