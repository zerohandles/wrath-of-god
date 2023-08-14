using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
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

    [Header("Tornado Settings")]
    public GameObject tornado;
    public Texture2D tornadoCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    [SerializeField] private Vector2 cursorOffset = Vector2.zero;
    private bool tornadoReady;


    private void Update()
    {
        // On left click fire a bullet towards the mouse position
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRate <= timer)
        {
            // Prevent firing if mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // Prevent firing is player is placing a tornado
            if (tornadoReady)
            {
                Tornado();
                return;
            }

            ShootLightning();
            timer = 0f;
        }
        
        // Right click to attempt to rescue an innocent enemy
        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            DetectInnocent();
        }
    }

    // Launch a lightning bolt towards the mouse's position
    void ShootLightning()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, dir);

        Instantiate(lightningBolt, firePoint.transform.position, Quaternion.Euler(0, 0, angle));   
    }

    // Summon a tornado at the mouse position
    public void Tornado()
    {
        // Change cursor to tornado cursor 
        if (!tornadoReady)
        {
            Cursor.SetCursor(tornadoCursor, cursorOffset, cursorMode);
            tornadoReady = true;
            return;
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(tornado, new Vector3(mousePosition.x, -2.25f, 0), Quaternion.identity);
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
        tornadoReady = false;
    }

    // Attempt to find an innocent enemy within the rescue radius of the mouse's position
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

    // Start the rescue animation and score the innocent rescued
    void RescueInnocent(GameObject target)
    {
        // Prevent multiple rescues at once to avoid graphical errors
        if (rescueInProgress)
        {
            return;
        }

        rescueInProgress = true;

        var innocent  = target.transform.GetComponent<Innocent>();
        innocent.PrepareForRescue();
        StartCoroutine(ActivateRescueLight(target));
        GameManager.instance.ScoreInnocent(target);
    }

    // Activate the line renderer rescue light on the passed in target
    IEnumerator ActivateRescueLight(GameObject target)
    {
        float timeElapsed = 0f;
        float lerpDuration = 1.5f;
        Vector3 lineStart = target.transform.position + new Vector3(0, 10, 0);
        Vector3 lineEnd = target.transform.position + new Vector3(0, -.5f, 0);

        rescueLight.SetPosition(0, lineStart);

        // Move the rescue light from offscreen to the target
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

        // Move the rescue light and target towards the top of the screen
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
