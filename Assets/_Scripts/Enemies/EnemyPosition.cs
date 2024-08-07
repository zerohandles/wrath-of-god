using UnityEngine;

public class EnemyPosition : MonoBehaviour
{
    private readonly float xBoundary = 8.5f;
    private bool isLeft;

    // Set the enemy's animation rotation and movement direction when reaching a screen boundary
    void Update()
    {
        if (transform.rotation.y == 1 || transform.rotation.y == -1)
            isLeft = true;
        else if (transform.rotation.y == 0)
            isLeft = false;

        if (isLeft && transform.position.x <= -xBoundary)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        if (!isLeft && transform.position.x >= xBoundary)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
