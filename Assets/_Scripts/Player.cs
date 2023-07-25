using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject lightningBolt;
    [SerializeField] private GameObject firePoint;
    [SerializeField] private float fireRate = 0.1f;
    private float timer;


    private void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireRate <= timer)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            ShootLightning();
            timer = 0f;
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
}
