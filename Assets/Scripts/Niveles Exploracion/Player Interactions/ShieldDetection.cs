using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject shieldObject; 
    [SerializeField] private AudioClip shieldActivationSound;
    private bool isShieldActive = false;

    private void Update()
    {
        // Check if right mouse button is held down
        if (Input.GetMouseButton(1)) // Right mouse button is 1
        {
            // Show the shield object
            ShowShield();
        }
        else
        {
            // Hide the shield object
            HideShield();
        }
    }

    private void ShowShield()
    {
        // Ensure the shield object is not null and check if it is not already active
        if (shieldObject != null && !isShieldActive)
        {
            // Enable the renderer or gameObject to make it visible
            shieldObject.SetActive(true);
            // Play the sound effect
            SoundFXManager.instance.PlaySoundEffectClip(shieldActivationSound, shieldObject.transform, 1f);
            // Set the shield as active
            isShieldActive = true;
        }
    }

    private void HideShield()
    {
        // Ensure the shield object is not null and check if it is currently active
        if (shieldObject != null && isShieldActive)
        {
            // Disable the renderer or gameObject to make it invisible
            shieldObject.SetActive(false);
            // Set the shield as inactive
            isShieldActive = false;
        }
    }
}
