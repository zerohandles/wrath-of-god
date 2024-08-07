using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    [Header("Pool Prefabs")]
    [SerializeField] LightningBolt lightningPrefab;
    ObjectPool<LightningBolt> lightningPool;

    [SerializeField] BackgroundMeteor backgroundMeteorPrefab;
    ObjectPool<BackgroundMeteor> backgroundMeteorPool;
    
    [SerializeField] Meteor meteorPrefab;
    ObjectPool<Meteor> meteorPool;


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
