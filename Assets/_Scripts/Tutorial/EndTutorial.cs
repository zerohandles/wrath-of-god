using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTutorial : MonoBehaviour
{
    public void StartLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }
}
