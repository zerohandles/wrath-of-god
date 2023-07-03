using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SpawnManager spawnManager;

    public float score;
    public bool gameIsActive;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        spawnManager = gameObject.GetComponent<SpawnManager>();
        gameIsActive = true;
    }

}
