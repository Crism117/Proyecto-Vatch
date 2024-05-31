using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject referenceObject; // Reference GameObject for movement boundaries
    public float moveSpeed = 5f; // Speed at which the enemy moves
    public float pauseDuration = 1f; // Duration to pause between movements
    public float movementBoxSize; // Box size
    private Vector2 minPosition; // Minimum position relative to the reference GameObject
    private Vector2 maxPosition; // Maximum position relative to the reference GameObject
    private Vector2 targetPosition; // Position to move towards
    private bool isPaused = false; // Flag to indicate if the enemy is currently paused
    private void Start()
    {
        // Calculate the movement boundaries relative to the reference GameObject
        if (referenceObject != null)
        {
            minPosition = referenceObject.transform.position - new Vector3(movementBoxSize, movementBoxSize); // Units to the left and down
            maxPosition = referenceObject.transform.position + new Vector3(movementBoxSize, movementBoxSize); // Units to the right and up
        }
        else
        {
            Debug.LogWarning("Reference GameObject not assigned for EnemyMovement!");
        }

        // Set the initial target position within the defined area
        SetRandomTargetPosition();
    }

    private void Update()
    {
        if (!isPaused)
        {
            // Move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the target position
            if ((Vector2)transform.position == targetPosition)
            {
                // Start a coroutine to pause before setting a new random target position
                StartCoroutine(PauseBeforeNextMovement());
            }
        }
    }

    private IEnumerator PauseBeforeNextMovement()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);
        isPaused = false;

        // Set a new random target position within the defined area
        SetRandomTargetPosition();
    }

    private void SetRandomTargetPosition()
    {
        // Generate a random target position within the defined area
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe box representing the movement boundaries around the reference GameObject
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((minPosition + maxPosition) / 2f, maxPosition - minPosition);
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Patient"))
        {
            targetPosition = transform.position;
        }
    }
}

