using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager : MonoBehaviour
{
    public Enemy[] enemies;
    public GameObject enemyContainer;
    public GameObject effectsContainer;

    private readonly float xBoundary = 9.5f;
    private readonly float groundOffset = -2.3f;

    private Vector2 spawnPos1;
    private Vector2 spawnPos2;


    void Start()
    {
        spawnPos1 = new Vector2(-xBoundary, groundOffset);
        spawnPos2 = new Vector2(xBoundary, groundOffset);
        
        // Create object pools for each enemy and each enemy's death particle effect.
        // Begin spawning enemies
        foreach (Enemy enemy in enemies)
        {
            ObjectPool<GameObject> pool = new ObjectPool<GameObject>(() =>
            {
                GameObject obj = Instantiate(enemy.enemyPrefab);
                obj.transform.SetParent(enemyContainer.transform);
                return obj;
            }, 
            enemyPrefab => enemyPrefab.SetActive(true),
            enemyPrefab => enemyPrefab.SetActive(false),
            enemyPrefab => Destroy(enemyPrefab),
            true,
            (int)enemy.spawnLimit,
            (int)enemy.spawnLimit + 10);

            ObjectPool<GameObject> effectPool = new ObjectPool<GameObject>(() =>
            {
                GameObject obj = Instantiate(enemy.deathEffect);
                obj.transform.SetParent(effectsContainer.transform);
                return obj;
            }, 
            effectPrefab => effectPrefab.SetActive(true),
            effectPrefab => effectPrefab.SetActive(false),
            effectPrefab => Destroy(effectPrefab),
            true, 
            (int)enemy.spawnLimit, 
            (int)enemy.spawnLimit + 20);

            PreFillPool(effectPool, (int)enemy.spawnLimit);

            StartCoroutine(SpawnEnemy(enemy, pool, effectPool));
        }
    }

    void PreFillPool(ObjectPool<GameObject> pool, int count)
    {
        List<GameObject> tempList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = pool.Get();
            tempList.Add(obj);
        }
        foreach (GameObject obj in tempList)
        {
            pool.Release(obj);
        }
    }

    // Continuously get enemies from thier pool and spawn them into the scene
    IEnumerator SpawnEnemy(Enemy enemy, ObjectPool<GameObject> prefabPool, ObjectPool<GameObject> effectPool)
    {
        // Stop from spawning more enemies than allowed in the scene
        if (enemy.totalSpawned >= enemy.maxSpawnable)
        {
            yield break;
        }

        // Spawn delay set for each enemy type
        yield return new WaitForSeconds(enemy.spawnDelay);

        // Spawn a new enemy if the on screen limit of that enemy type is not currently exceeded
        if (enemy.spawnLimit > GameObject.FindGameObjectsWithTag(enemy.tag).Length && !GameManager.instance.isGameOver)
        {
            int spawnLocation = Random.Range(0, 2);
            GameObject obj = prefabPool.Get();
            
            // Pass the enemy's prefab and death effect pool to the EnemyDeathEffect script for Release on death
            EnemyDeathEffect death = obj.GetComponent<EnemyDeathEffect>();
            death.SetPool(prefabPool, effectPool);

            // Place new enemy on 1 of the 2 outer edges of the screen
            if (spawnLocation == 0)
                obj.transform.SetPositionAndRotation(spawnPos1, Quaternion.Euler(0, 0, 0));
            else
                obj.transform.SetPositionAndRotation(spawnPos2, Quaternion.Euler(0, 180, 0));
            enemy.totalSpawned += 1;
        }

        StartCoroutine(SpawnEnemy(enemy, prefabPool, effectPool));
    }

    // Get the enemy death particle effect from its pool
    public void SpawnEfect(ObjectPool<GameObject> pool, Vector3 location, float timer)
    {
        var effect = pool.Get();
        effect.transform.position = location;
        StartCoroutine(EffectTimer(pool, effect, timer));
    }

    // Release the death particle effect back to its pool
    private IEnumerator EffectTimer(ObjectPool<GameObject> pool, GameObject effect, float timer)
    {
        yield return new WaitForSeconds(timer);
        pool.Release(effect);
    }
}
