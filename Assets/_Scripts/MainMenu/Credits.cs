using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditsGO;

    public void ShowCredits()
    {
        creditsGO.SetActive(!creditsGO.activeSelf);
    }

    public void HideCredits()
    {
        creditsGO.SetActive(!creditsGO.activeSelf);
    }
}
