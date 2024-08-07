using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public float value;
    public float spawnDelay;
    public float spawnLimit;
    public string tag;
    public GameObject deathEffect;
    public float totalSpawned;
    public float maxSpawnable = Mathf.Infinity;
}
