using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Health entityHealth; // This should be assigned in the Unity Editor

    private void Start()
    {
        // Check if references are assigned
        if (entityHealth == null)
        {
            Debug.LogError("Health component not assigned!");
            return; // Exit the Start method early if entityHealth is not assigned
        }
        healthBar = GetComponent<Slider>();
        // Check if Slider component is found
        if (healthBar == null)
        {
            Debug.LogError("Slider component not found!");
            return; // Exit the Start method early if healthBar is not found
        }
        // Set the max value of the health bar to the entity's max health
        healthBar.maxValue = entityHealth.maxHealth;
        // Set the initial health value of the health bar
        healthBar.value = entityHealth.curHealth;
    }
    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }
}
