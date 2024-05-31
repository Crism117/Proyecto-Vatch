using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSummoner : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // Prefab of the projectile
    public float projectileSpeed = 10f; // Speed of the projectile
    public int destroyDistance;// Distance from the origin at which the projectile is distroyed
    public float fireFrequency = 1f; // Frequency of firing projectiles (projectiles per second)
    private float fireTimer; // Timer to control the firing frequency
    [Header("Attack Settings")]
    public Transform aimTarget; // GameObject to aim the projectile at
    public string attack;// Tag of object that is able to hit the object
    public int projectileDamage = 10; // Amount of damage inflicted by the projectile
    private float attackAnimationLength;
    private bool isAttacking;
    public DamageSpotState DamSpotChageType;
    [Header("Stunt Settings")]
    public string stunt; //Tag of object that is going to  be stunt by the projectile
    public float timeStunted;
    private void Start()
    {
        // Initialize the fire timer
        fireTimer = 1f / fireFrequency;
    }
    private void Update()
    {
        if (!isAttacking) 
        { 
            // Update the fire timer
            fireTimer -= Mathf.Clamp(Time.deltaTime, 0, 1f / fireFrequency);
            // If the fire timer reaches zero or below, fire a projectile
            if (fireTimer <= 0f)
            {
                StartCoroutine(PauseBeforeNextMovement());
                // Reset the fire timer
                fireTimer = 1f / fireFrequency;
            }
        }
    }
    private void FireProjectile()
    {
        if (projectilePrefab != null && aimTarget != null)
        {
            // Calculate the direction towards the aim target
            Vector3 direction = (aimTarget.position - transform.position).normalized;

            // Instantiate the projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            // Check if the Projectile component exists
            if (projectileScript != null)
            {
                if (projectileScript.shootingSound != null)
                {
                    SoundFXManager.instance.PlayRandomSoundEffectClip(projectileScript.shootingSound, projectile.transform, 1f);
                }// Pass necessary information to the Projectile
                projectileScript.SetOrigin(gameObject, projectileDamage, destroyDistance, attack, stunt, timeStunted, DamSpotChageType);
            }
            // Set the velocity of the projectile to move towards the aim target
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
                // Calculate the rotation angle based on the direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Apply the rotation to the projectile
                projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                Debug.LogWarning("Projectile Prefab does not have a Rigidbody2D component!");
            }
        }
        else
        {
            Debug.LogWarning("Projectile Prefab or Aim Target not assigned!");
        }
    }
    public void StartAnimation() 
    {
        Animator animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned.");
        }
        else
        {
            // Retrieve the RuntimeAnimatorController from the Animator
            RuntimeAnimatorController rac = animator.runtimeAnimatorController;

            // Iterate through all AnimationClips in the controller
            foreach (AnimationClip clip in rac.animationClips)
            {
                if (clip.name == "Attack")
                {
                    attackAnimationLength = clip.length;
                }
            }
        }
        animator.SetTrigger("Attack");
    }
    private IEnumerator PauseBeforeNextMovement()
    {
        StartAnimation();
        isAttacking = true;
        yield return new WaitForSeconds(attackAnimationLength);
        isAttacking = false;
        // Set a new random target position within the defined area
        FireProjectile();
    }
}
