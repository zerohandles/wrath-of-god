using UnityEngine;

public class BackgrounddMeteorSpawner : MonoBehaviour
{
    [Header("Spawn Info")]
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] float meteorSpawnDelay;

    readonly float spawnPosY = 7;
    readonly float spawnPosXRange = 7;

    void OnEnable() => InvokeRepeating(nameof(SpawnMeteor), 1, meteorSpawnDelay);

    // Spawn a meteor along the top  of the screen
    void SpawnMeteor()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnPosXRange, spawnPosXRange), spawnPosY, 0);

        var meteor = PoolManager.Instance.GetBackgroundMeteor();
        meteor.transform.position = spawnPos;
    }

}
