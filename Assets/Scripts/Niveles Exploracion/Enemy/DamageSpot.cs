using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSpot : MonoBehaviour
{
    private GameObject patient;
    [SerializeField] public AudioClip hurtSound;

    void Start()
    {
        patient = GameObject.FindWithTag("Patient");
        
        if (patient != null)
        {
            // Do something with the patient object
        }
        else
        {
            Debug.LogWarning("No object with tag 'Patient' found in the scene.");
        }
    }
    private void Update()
    {
        if (TryGetComponent(out Animator SetState))
        {
            if (SetState.GetFloat("State") == 0) 
            {
                Destroy(gameObject);
            }
        }
    }
    public void ChangeHealth(int amount) 
    {
        if (patient.TryGetComponent(out Health health))
        {
            health.ChangeHealth(amount);
        }
    }
}
