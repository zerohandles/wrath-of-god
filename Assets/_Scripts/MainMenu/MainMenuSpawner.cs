using System.Collections;
using UnityEngine;

// Simplified spawn manager for the main menu background animation
public class MainMenuSpawner : MonoBehaviour
{
    public Enemy[] enemies;
    public GameObject enemyContainer;

    private float xBoundary = 9.5f;
    private float groundOffset = -2.3f;

    private Vector2 spawnPos1;
    private Vector2 spawnPos2;


    void Start()
    {
        spawnPos1 = new Vector2(-xBoundary, groundOffset);
        spawnPos2 = new Vector2(xBoundary, groundOffset);
        foreach (Enemy enemy in enemies)
        {
            StartCoroutine(SpawnEnemy(enemy));
        }
    }

    // Spawn enemies along the edges of the screen
    IEnumerator SpawnEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(enemy.spawnDelay);

        if (enemy.spawnLimit > GameObject.FindGameObjectsWithTag(enemy.tag).Length)
        {
            int spawnLocation = Random.Range(0, 2);

            if (spawnLocation == 0)
            {
                GameObject obj = (GameObject)Instantiate(enemy.enemyPrefab, spawnPos1, Quaternion.Euler(0, 0, 0));
                obj.transform.SetParent(enemyContainer.transform);
            }
            else
            {
                GameObject obj = (GameObject)Instantiate(enemy.enemyPrefab, spawnPos2, Quaternion.Euler(0, 180, 0));
                obj.transform.SetParent(enemyContainer.transform);
            }
        }

        StartCoroutine(SpawnEnemy(enemy));
    }
}
