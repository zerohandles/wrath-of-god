using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    private Vector3 startingPos = new Vector3(0, -6.5f, 0);
    [SerializeField] Vector3 endPos = new Vector3 (0, 0.3f, 0);
    [SerializeField] float speed;

    private readonly float pauseTime = 3;
    private bool isRising = false;
    private Vector3 target;


    void OnEnable()
    {
        transform.position = startingPos;
        target = endPos;
        isRising = false;
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
        isRising = false;
    }
}
