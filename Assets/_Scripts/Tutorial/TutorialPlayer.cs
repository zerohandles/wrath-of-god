using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


// Simplified player script for the turorial level.  See player script for additional comments
public class TutorialPlayer : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireRate = 0.1f;
    private float timer;

    [Header("Rescue Innocents")]
    [SerializeField] private float rescueRadius = .5f;
    [SerializeField] ContactFilter2D rescueLayerMask;
    [SerializeField] private LineRenderer rescueLight;
    private bool rescueInProgress = false;
    Collider2D[] result = new Collider2D[1];

    public bool canShoot;

    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRate <= timer)
        {
            if (EventSystem.current.IsPointerOverGameObject() || !canShoot) 
            {
                return;
            }

            ShootLightning();
            timer = 0f;
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            DetectInnocent();
        }
    }

    void ShootLightning()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);

        Instantiate(lightningBolt, firePoint.transform.position, Quaternion.Euler(0, 0, angle));
    }

    void DetectInnocent()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        System.Array.Clear(result, 0, result.Length);

        Physics2D.OverlapCircle(mousePosition, rescueRadius, rescueLayerMask, result);

        foreach (Collider2D c in result)
        {
            if (c == null)
            {
                return;
            }

            GameObject innocent = c.gameObject;
            RescueInnocent(innocent);
        }
    }

    void RescueInnocent(GameObject target)
    {
        if (rescueInProgress)
        {
            return;
        }

        rescueInProgress = true;

        var innocent = target.transform.GetComponent<Innocent>();
        innocent.PrepareForRescue();
        StartCoroutine(ActivateRescueLight(target));
        GameManager.instance.score += 5000;
        GameManager.instance.UpdateUI();
    }

    IEnumerator ActivateRescueLight(GameObject target)
    {
        float timeElapsed = 0f;
        float lerpDuration = 1.5f;
        Vector3 lineStart = target.transform.position + new Vector3(0, 10, 0);
        Vector3 lineEnd = target.transform.position + new Vector3(0, -.5f, 0);

        rescueLight.SetPosition(0, lineStart);

        while (timeElapsed < lerpDuration)
        {
            float factor = timeElapsed / lerpDuration;

            rescueLight.SetPosition(1, Vector3.Lerp(lineStart, lineEnd, factor));

            timeElapsed += Mathf.Min(Time.deltaTime, lerpDuration - timeElapsed); 

            yield return null;
        }

        rescueLight.SetPosition(1, lineEnd);
        timeElapsed = 0f;

        yield return new WaitForSeconds(.5f);

        while (timeElapsed < lerpDuration)
        {
            float factor = timeElapsed / lerpDuration;

            rescueLight.SetPosition(1, Vector3.Lerp(lineEnd, lineStart, factor));
            target.transform.position = Vector3.Lerp(target.transform.position, lineStart, factor * 0.1f);

            timeElapsed += Mathf.Min(Time.deltaTime, lerpDuration - timeElapsed);

            yield return null;
        }

        rescueInProgress = false;
        Destroy(target);
    }
}
