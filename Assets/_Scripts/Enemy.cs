using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int value;
    public float spawnRate;
    public float spawnLimit;
    public string tag;
}
