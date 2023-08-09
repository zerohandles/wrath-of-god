using System.Collections;
using UnityEngine;

public class Flood : MonoBehaviour
{
    private Vector3 startingPos = new Vector3(0, -6.5f, 0);
    private Vector3 endPos = new Vector3(0, 0.3f, 0);
    [SerializeField] float speed;

    private readonly float pauseTime = 3;
    private bool isRising = false;
    private Vector3 target;

    private FadeEffectAudio fader;
    private float targetVolume;
    private readonly float lerpDuration = 5;




    void OnEnable()
    {
        transform.position = startingPos;
        target = endPos;
        isRising = false;
        targetVolume = .5f;
        StartCoroutine(fader.FadeAudio(targetVolume, lerpDuration));
    }

    private void Awake()
    {
        fader = GetComponent<FadeEffectAudio>();
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.0001f && !isRising)
        {
            isRising = true;
            StartCoroutine(PauseMovement());

            if (target == startingPos)
            {
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator PauseMovement()
    {
        yield return new WaitForSeconds(pauseTime);
        target = startingPos;
        targetVolume = 0;
        isRising = false;
        StartCoroutine(fader.FadeAudio(targetVolume, lerpDuration));
    }

}
