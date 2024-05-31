using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    const int hint = 1;
    public bool interactive;
    [HideInInspector]
    public bool collected;
    private GameObject hintIcon;
    public GameObject hintPrefab;
    [SerializeField] public AudioClip hintCollectSound;
    private void Start()
    {
        collected = false;
        hintIcon = GameObject.FindGameObjectWithTag("Hint Icon");
    }
    private void Update()
    {
        if (collected)
        {
            if (hintCollectSound != null)
            {
                SoundFXManager.instance.PlaySoundEffectClip(hintCollectSound, transform, 1f);
            }
            interactive = false;
            if (hintIcon != null && hintIcon.TryGetComponent(out Animator collectAnimation))
            { 
                collectAnimation.SetTrigger("Collect");
            }
            Destroy(gameObject);
            GameManagerScript.Instance.AddHint(hint);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& interactive )
        {
            collected = true;
            interactive = false;
        }
    }
}
