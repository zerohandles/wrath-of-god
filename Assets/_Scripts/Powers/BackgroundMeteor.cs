using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMeteor : MonoBehaviour
{
    private Vector3 target;
    private readonly float targetXRange = 8.3f;
    private readonly float targetY = -2;
    private readonly float lifeTime = 2;
    private float timer = 0;

    [SerializeField] private float speed;

    // Set a random target angle to move along
    private void OnEnable()
    {
        target = new Vector3(Random.Range(-targetXRange, targetXRange), targetY, 0);
        Vector3 dir = (target - transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector3.right, dir);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        if (timer > lifeTime)
        {
            // TODO: add object pooling for meteors
            Destroy(gameObject);
        }

        transform.Translate(speed * Time.deltaTime * Vector3.right, Space.Self);
        timer += Time.deltaTime;
    }
}
