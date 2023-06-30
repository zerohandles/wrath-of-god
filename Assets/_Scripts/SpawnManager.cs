using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Enemy[] enemies;
    public float startDelay = 1f;
    public float spawnDelay = 1f;
    private float xBoundary = 9.5f;
    private float groundOffset = -2.5f;

    private Vector2 spawnPos1;
    private Vector2 spawnPos2;


    // Start is called before the first frame update
    void Start()
    {
        spawnPos1 = new Vector2(-xBoundary, groundOffset);
        spawnPos2 = new Vector2(xBoundary, groundOffset);
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        int spawnLocation = Random.Range(0, 2);
        
        foreach (Enemy enemy in enemies)
        {
            if (enemy.spawnLimit <= GameObject.FindGameObjectsWithTag(enemy.tag).Length)
            {
                return;
            }

            if (spawnLocation == 0)
            {
                Instantiate(enemy.enemyPrefab, spawnPos1, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(enemy.enemyPrefab, spawnPos2, Quaternion.Euler(0, 180, 0));
            }
        }
    }
}
