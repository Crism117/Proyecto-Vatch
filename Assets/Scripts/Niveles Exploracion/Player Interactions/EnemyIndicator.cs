using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private Camera mainCamera;
    [HideInInspector]
    public GameObject target;

    public GameObject arrow; // Assign the arrow GameObject in the Inspector

    void Start()
    {
        // Get the main camera
        mainCamera = Camera.main;
        target = GameObject.FindGameObjectWithTag("Enemy");
    }

    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Enemy")) 
        { 
            target = GameObject.FindGameObjectWithTag("Damage Spot"); 
        }

        if (target == null)
        {
            arrow.SetActive(false);
            return;
        }

        bool enemyOnScreen = IsEnemyOnScreen(target);
        if (!enemyOnScreen)
        {
            arrow.SetActive(true);
            PointArrowAtEnemy(target);
        }
        else
        {
            arrow.SetActive(false);
        }
    }

    bool IsEnemyOnScreen(GameObject enemy)
    {
        Vector2 screenPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    void PointArrowAtEnemy(GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - mainCamera.transform.position;
        direction.z = 0; // Ensure we're working in 2D space

        // Normalize the direction vector
        direction.Normalize();

        // Calculate the angle in radians and then convert to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
