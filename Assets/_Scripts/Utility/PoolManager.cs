using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    ObjectPool<LightningBolt> lightningPool;
    [SerializeField] LightningBolt lightningPrefab;

    ObjectPool<BackgroundMeteor> backgroundMeteorPool;
    [SerializeField] BackgroundMeteor backgroundMeteorPrefab;

    ObjectPool<Meteor> meteorPool;
    [SerializeField] Meteor meteorPrefab;


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

        backgroundMeteorPool = new ObjectPool<BackgroundMeteor>(
            () =>
            {
                var meteor = Instantiate(backgroundMeteorPrefab);
                meteor.SetPool(backgroundMeteorPool);
                return meteor;
            },
            t => t.gameObject.SetActive(true),
            t => t.gameObject.SetActive(false)
            );

        meteorPool = new ObjectPool<Meteor>(
            () =>
            {
                var meteor = Instantiate(meteorPrefab);
                meteor.SetPool(meteorPool);
                return meteor;
            },
            t => t.gameObject.SetActive(true),
            t => t.gameObject.SetActive(false)
            );
    }


    public LightningBolt GetLightningBolt() => lightningPool.Get();

    public BackgroundMeteor GetBackgroundMeteor() => backgroundMeteorPool.Get();

    public Meteor GetMeteor() => meteorPool.Get();
}
