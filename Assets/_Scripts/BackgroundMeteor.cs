using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMeteor : MonoBehaviour
{
    private Vector3 target;
    private float targetXRange = 8.3f;
    private float targetY = -2;
    private float timer = 0;
    private float lifeTime = 2;

    [SerializeField] private float speed;


    private void OnEnable()
    {
        target = new Vector3(Random.Range(-targetXRange, targetXRange), targetY, 0);
        Vector3 dir = (target - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector3.right, dir);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Start()
    {

    }

    private void Update()
    {
        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
        timer += Time.deltaTime;
    }
}
