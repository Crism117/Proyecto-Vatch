using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject projectileOrigin;
    private float destroyDistance;
    private Transform originTransform;
    private int damage;
    private string attack;
    private string stunt;
    private float stuntTimer;
    private string damSpotState;
    private string hint;
    [SerializeField] AudioClip shieldHitSound;
    [SerializeField] public AudioClip[] shootingSound;
    [SerializeField] AudioClip stuntSound;
    public void SetOrigin(GameObject origin, int damageAmount, float distance, string objectAttacked, string objectStunt, float timeStunted, DamageSpotState changeType)
    {
        projectileOrigin = origin;
        damage = damageAmount;
        destroyDistance = distance;
        attack = objectAttacked;
        stunt = objectStunt;
        originTransform = origin.transform;
        stuntTimer = timeStunted;
        damSpotState = changeType.ToString();
        hint = "Hint";
    }

    private void Update()
    {
        if (originTransform != null)
        {
            float distance = Vector2.Distance(transform.position, originTransform.position);
            if (distance > destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D objectHit)
    {
        switch (objectHit.tag) 
        {
            case string tag when tag == attack:                                         //When colling with Entity tag asigned in "Attack"
                if (objectHit.TryGetComponent(out DamagePointSummoner hitPoint)) 
                {
                    hitPoint.SummonDamage(transform.position);
                }
                if (objectHit.TryGetComponent(out Health health)) 
                {
                    health.ChangeHealth(damage);
                    Destroy(gameObject);
                    if (objectHit.TryGetComponent(out Animator attackedAnimator))
                    {
                        attackedAnimator.SetTrigger("Hit");
                    }
                }
                
                break;          
            case "Shield":                                                          //When colliding with Projectile
                if (shieldHitSound != null)
                { 
                    SoundFXManager.instance.PlaySoundEffectClip(shieldHitSound, objectHit.transform, 1f);
                }
                goto case "Projectile";
            case "Projectile":
                for (int i = objectHit.transform.childCount - 1; i >= 0; i--)
                {
                    Transform child = objectHit.transform.GetChild(i);
                    // Check if the child has the specified tag
                    if (child.CompareTag(hint)&& !projectileOrigin.CompareTag("Player"))
                    {
                        if (child.TryGetComponent(out Hint hintScript))
                        {
                            hintScript.collected = true;
                            //Destroy(child.GameObject());
                        }
                    }
                }//When colliding with Shield
                Destroy(gameObject);
                if (objectHit.TryGetComponent(out Animator shieldAnimator))
                {
                    shieldAnimator.SetTrigger("Hit");
                }
                break;
            case string tag when tag == stunt:                                          //When colling with Entity tag asigned in "Stunt"
                if (stuntSound != null)
                {
                    SoundFXManager.instance.PlaySoundEffectClip(stuntSound, objectHit.transform, 1f);
                }
                if (objectHit.TryGetComponent(out ProjectileSummoner stuntShooting))
                {
                    stuntShooting.cooldownTimer = stuntTimer;
                }
                if (objectHit.TryGetComponent(out Animator stuntAnimator))
                {
                    stuntAnimator.SetTrigger("Stunt");
                }
                if (objectHit.TryGetComponent(out PlayerMovement stuntMovement)) 
                {
                    stuntMovement.coolDownTimer = stuntTimer;
                }
                Destroy(gameObject);
                break;
            case "Damage Spot":
            case string checkTag when checkTag == tag.ToString():
                float stateChange = damSpotState == "heal" ? -1f : damSpotState == "hurt" ? 1f : 0;
                
                if (objectHit.TryGetComponent(out Animator SetState))
                {
                    
                    SetState.SetFloat("State", SetState.GetFloat("State")+stateChange);
                }
                if (objectHit.TryGetComponent(out DamageSpot damageSpotScript))
                {
                    damageSpotScript.ChangeHealth((damage * (int)stateChange)/4);
                }
                if (damageSpotScript.hurtSound != null&&damSpotState=="hurt")
                {
                    SoundFXManager.instance.PlaySoundEffectClip(damageSpotScript.hurtSound, objectHit.transform, 1f);
                }
                Destroy(gameObject);
                break;
            default:
                break;
                
        }
        if (!projectileOrigin.CompareTag("Enemy"))
        {
            for (int i = objectHit.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = objectHit.transform.GetChild(i);
                // Check if the child has the specified tag
                if (child.CompareTag(hint))
                {
                    if (child.TryGetComponent(out Hint hintScript))
                    {
                        hintScript.collected = true;
                       // Destroy(child.GameObject());
                    }
                }
            }
        }
    }
}
