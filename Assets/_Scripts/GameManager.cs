using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SpawnManager spawnManager;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        spawnManager = gameObject.GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
