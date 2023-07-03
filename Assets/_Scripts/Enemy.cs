using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public float value;
    public float spawnDelay;
    public float spawnLimit;
    public string tag;

}
