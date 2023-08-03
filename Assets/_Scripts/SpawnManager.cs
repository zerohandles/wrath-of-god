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

            ObjectPool<GameObject> effectPool = new ObjectPool<GameObject>(() =>
            {
                GameObject obj = Instantiate(enemy.deathEffect);
                obj.transform.SetParent(effectsContainer.transform);
                return obj;
            }, effectPrefab =>
            {
                effectPrefab.gameObject.SetActive(true);
            }, effectPrefab =>
            {
                effectPrefab.gameObject.SetActive(false);
            }, effectPrefab =>
            {
                Destroy(effectPrefab.gameObject);
            }, true, (int)enemy.spawnLimit, (int)enemy.spawnLimit + 20);

            StartCoroutine(SpawnEnemy(enemy, pool, effectPool));
        }
    }

    IEnumerator SpawnEnemy(Enemy enemy, ObjectPool<GameObject> prefabPool, ObjectPool<GameObject> effectPool)
    {
        if (enemy.totalSpawned >= enemy.maxSpawnable)
        {
            yield break;
        }

        yield return new WaitForSeconds(enemy.spawnDelay);

        if (enemy.spawnLimit > GameObject.FindGameObjectsWithTag(enemy.tag).Length && !GameManager.instance.isGameOver)
        {
            int spawnLocation = Random.Range(0, 2);
            GameObject obj = prefabPool.Get();
            EnemyDeathEffect death = obj.gameObject.GetComponent<EnemyDeathEffect>();
            death.prefabPool = prefabPool;
            death.effectPool = effectPool;

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

        StartCoroutine(SpawnEnemy(enemy, prefabPool, effectPool));
    }

    public void SpawnEfect(ObjectPool<GameObject> pool, Vector3 location, float timer)
    {
        var effect = pool.Get();
        effect.transform.position = location;
        StartCoroutine(EffectTimer(pool, effect, timer));
    }

    private IEnumerator EffectTimer(ObjectPool<GameObject> pool, GameObject effect, float timer)
    {
        yield return new WaitForSeconds(timer);
        pool.Release(effect);
    }
}
