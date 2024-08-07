using UnityEngine;
using UnityEngine.Pool;

public class BackgroundMeteor : MonoBehaviour
{
    [SerializeField] float speed;
    
    ObjectPool<BackgroundMeteor> _pool;
    Vector3 target;
    readonly float targetXRange = 8.3f;
    readonly float targetY = -3.5f;
    readonly float lifeTime = 2;
    float timer = 0;

    // Set a random target angle to move along
    void OnEnable()
    {
        target = new Vector3(Random.Range(-targetXRange, targetXRange), targetY, 0);
        Vector3 dir = (target - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector3.right, dir);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        if (timer > lifeTime)
        {
            timer = 0;
            _pool.Release(this);
        }

        Vector3 direction = (target - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        timer += Time.deltaTime;
    }

    public void SetPool(ObjectPool<BackgroundMeteor> pool) => _pool = pool;
}
