using UnityEngine;

public class LevelSelectMouseHover : MonoBehaviour
{
    public GameObject toolTip;

    public void ShowToolTip() => toolTip.SetActive(true);

    public void HideToolTip() => toolTip.SetActive(false);
}
