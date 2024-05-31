using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    [HideInInspector]
    public float curHealth = 0;
    public float maxHealth = 100;
    public HealthBar healthBar;
    public GameObject itemDrop;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip healSound;
    [SerializeField] private AudioClip deathSound;
    void Start()
    {
        curHealth = maxHealth;
    }
    public void ChangeHealth(int damage)
    {
        curHealth = Mathf.Clamp(curHealth + damage,0,maxHealth);
        if (damage > 0 && healSound != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(healSound, transform, 1f);
        }
        else if (hurtSound != null)
        {
            SoundFXManager.instance.PlaySoundEffectClip(hurtSound, transform, 1f);
        }
        healthBar.SetHealth(curHealth);
        if ((curHealth <= 0) && (!CompareTag("Patient")))
        {
            if (deathSound != null)
            {
                SoundFXManager.instance.PlaySoundEffectClip(deathSound, transform, 1f);
            }
            Destroy(gameObject);
            if (itemDrop != null)
            {
                GameObject droppedItem = Instantiate(itemDrop, transform.position, Quaternion.identity);
                Hint hintScript = droppedItem.GetComponent<Hint>();

                // Check if the Projectile component exists
                if (hintScript != null)
                {
                    // Pass necessary information to the Projectile
                    hintScript.interactive = true;
                }
            }
        }
        ProjectileSummoner playerSummonerScript = GameObject.FindGameObjectWithTag("Player")?.GetComponent<ProjectileSummoner>();

        if (playerSummonerScript != null && CompareTag("Enemy"))
        {
            if (curHealth + playerSummonerScript.projectileDamage <= 0)
            {
                playerSummonerScript.lastShot = true;
            }
        }
        Debug.Log(gameObject.name +"'s health is:\n" + curHealth);
    }
}