using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    private float spawnPosY = 7;
    private float spawnPosXRange = 7;

    [SerializeField] private float meteorSpawnDelay;
    [SerializeField] private float meteorStormTimer;

    public GameObject meteor;

    private void OnEnable()
    {
        InvokeRepeating(nameof(SpawnMeteor), 1, meteorSpawnDelay);
        StartCoroutine(MeteorStormTimer());
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosXRange, spawnPosXRange), spawnPosY, 0);

        Instantiate(meteor, spawnPos, Quaternion.identity);
    }

    IEnumerator MeteorStormTimer()
    {
        yield return new WaitForSeconds(meteorStormTimer);
        CancelInvoke(nameof(SpawnMeteor));
        gameObject.SetActive(false);
    }
}
