using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject creditsGO;

    public void ShowCredits() => creditsGO.SetActive(!creditsGO.activeSelf);

    public void HideCredits() => creditsGO.SetActive(!creditsGO.activeSelf);
}
