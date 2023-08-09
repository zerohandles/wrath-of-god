using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    public void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void UnlockEverything()
    {
        PlayerPrefs.SetInt("levelReached", 6);
        PlayerPrefs.SetInt("level1", 1);
        PlayerPrefs.SetInt("level2", 1);
        PlayerPrefs.SetInt("level3", 1);
        PlayerPrefs.SetInt("level4", 1);
    }

}
