using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    ObjectPool<LightningBolt> lightningPool;
    [SerializeField] LightningBolt lightningPrefab;


    public static PoolManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        lightningPool = new ObjectPool<LightningBolt>(
            () =>
            {
                var shot = Instantiate(lightningPrefab);
                shot.SetPool(lightningPool);
                return shot;
            },
            t => t.gameObject.SetActive(true),
            t => t.gameObject.SetActive(false)
            );
    }


    public LightningBolt GetLightningBolt() => lightningPool.Get();
}
