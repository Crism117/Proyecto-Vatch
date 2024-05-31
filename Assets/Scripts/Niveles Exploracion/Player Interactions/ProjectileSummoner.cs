using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DamageSpotState
{
heal,
hurt
}
public class ProjectileSummoner : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject defaultProjectile;
    public GameObject secondaryProjectile;
    [HideInInspector]
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f; // Speed of the projectile
    public float destroyDistance = 20f; // Distance from the origin at which the projectile is destroyed
    public float cooldownDuration = 0.5f; // Cooldown duration in seconds
    [HideInInspector]
    public float cooldownTimer = 0f; // Timer to control the cooldown
    public float maxCoolDownTime;
    [HideInInspector]
    public bool lastShot;
    [Header("Attack Settings")]
    public int projectileDamage; // Amount of damage inflicted by the projectile
    public string attack;
    public DamageSpotState DamSpotChageType;
    private void Start()
    {
        projectilePrefab = defaultProjectile;
        lastShot = false;
    }
    void Update()
    {
        if (lastShot) 
        {
            projectilePrefab = secondaryProjectile;
        }
        // Update the cooldown timer
        cooldownTimer -= Mathf.Clamp(Time.deltaTime,0, maxCoolDownTime);
        
        // Check if the cooldown has expired and the player can shoot
        if (cooldownTimer <= 0f)
        {
            // Check if the player can shoot
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Summon the projectile
                ShortClick();
                // Reset the cooldown timer
                cooldownTimer = cooldownDuration;
            }
        } //cooldown
    }
    void ShortClick()
    {
        // Calculate the direction from the player to the mouse cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Instantiate the projectile Prefab
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        // Check if the Projectile component exists
        if (projectileScript != null)
        {
            if (projectileScript.shootingSound != null)
            {
                SoundFXManager.instance.PlayRandomSoundEffectClip(projectileScript.shootingSound, projectile.transform, 1f);
            }
            // Pass necessary information to the Projectile
            projectileScript.SetOrigin(gameObject, projectileDamage, destroyDistance, attack, null, 0f, DamSpotChageType);
        }

        // Get the Rigidbody2D component of the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Check if the projectile has a Rigidbody2D component
        if (rb != null)
        {
            // Set the velocity of the projectile to move in the calculated direction
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
}

