using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{
    [SerializeField] float startingYPos = -6.5f;
    [SerializeField] float maxYPos = 0.3f;
    [SerializeField] float speed;

    private readonly float timeAtMaxHeight = 3;
    private bool maxHeightReached = false;
    private Vector3 target;


    void OnEnable()
    {
        transform.position = new Vector3(0, startingYPos, 0);
        target = new Vector3(0, maxYPos,0);
        maxHeightReached = false;
    }

    void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.01f && !maxHeightReached)
        {
            maxHeightReached = true;
            StartCoroutine(PauseMovement());
        }
    }

    IEnumerator PauseMovement()
    {
        yield return new WaitForSeconds(3);
        target = new Vector3(0, startingYPos, timeAtMaxHeight);
    }
}
