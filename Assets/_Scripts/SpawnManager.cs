using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    public Enemy[] enemies;
    public GameObject enemyContainer;

    private readonly float xBoundary = 9.5f;
    private readonly float groundOffset = -2.3f;

    private Vector2 spawnPos1;
    private Vector2 spawnPos2;


    void Start()
    {
        spawnPos1 = new Vector2(-xBoundary, groundOffset);
        spawnPos2 = new Vector2(xBoundary, groundOffset);
        foreach (Enemy enemy in enemies)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(() =>
            {
                GameObject obj = Instantiate(enemy.enemyPrefab);
                obj.transform.SetParent(enemyContainer.transform);
                return obj;
            }, enemyPrefab =>
            {
                enemyPrefab.gameObject.SetActive(true);
            }, enemyPrefab =>
            {
                enemyPrefab.gameObject.SetActive(false);
            }, enemyPrefab =>
            {
                Destroy(enemyPrefab.gameObject);
            }, true, (int)enemy.spawnLimit, (int)enemy.spawnLimit + 10); 

            StartCoroutine(SpawnEnemy(enemy, pool));
        }
    }

    IEnumerator SpawnEnemy(Enemy enemy, ObjectPool<GameObject> pool)
    {
        if (enemy.totalSpawned >= enemy.maxSpawnable)
        {
            yield break;
        }

        yield return new WaitForSeconds(enemy.spawnDelay);

        if (enemy.spawnLimit > GameObject.FindGameObjectsWithTag(enemy.tag).Length && !GameManager.instance.isGameOver)
        {
            int spawnLocation = Random.Range(0, 2);
            GameObject obj = pool.Get();
            EnemyDeathEffect death = obj.gameObject.GetComponent<EnemyDeathEffect>();
            death.pool = pool;

            if (spawnLocation == 0)
            {
                obj.transform.SetPositionAndRotation(spawnPos1, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                obj.transform.SetPositionAndRotation(spawnPos2, Quaternion.Euler(0, 180, 0));
            }
            enemy.totalSpawned += 1;
        }

        StartCoroutine(SpawnEnemy(enemy, pool));
    }

    public void KillEnemy(ObjectPool<GameObject> pool, EnemyDeathEffect enemy)
    {
        pool.Release(enemy.gameObject);
    }
}
