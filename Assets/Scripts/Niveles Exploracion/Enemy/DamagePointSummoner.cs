using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class DamagePointSummoner : MonoBehaviour
{
    public GameObject damagePointPrefab;
    public float spawnBoxSize; // Box size
    private Vector2 minPosition; // Minimum position relative to the reference GameObject
    private Vector2 maxPosition; // Maximum position relative to the reference GameObject
    private Vector2 targetPosition; // Position to move towards
    private Vector2 randomSpawnPoint; //Random Point where DamagePoint will spawn
    public int maxNumberOfDamZones;
    public Vector2 nearSpotsBoxCheckSize = new Vector2(4,4);
    private bool nearbySpotDetected;
    public void SummonDamage(Vector2 hitPosition) 
    {
        minPosition = hitPosition - new Vector2(spawnBoxSize, spawnBoxSize); // Units to the left and down
        maxPosition = hitPosition + new Vector2(spawnBoxSize, spawnBoxSize); // Units to the right and up
        //retrySpawn:
        randomSpawnPoint = new Vector2(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y)); // Generate a random target position within the defined area
        targetPosition = randomSpawnPoint;
        RaycastHit2D[] hitResults = Physics2D.BoxCastAll(targetPosition, nearSpotsBoxCheckSize, 0, new Vector2(0,0), 0, Physics2D.DefaultRaycastLayers, -Mathf.Infinity, Mathf.Infinity);
        for (int i = 0; i < hitResults.Length; i++) 
        {
            if (hitResults[i].collider.gameObject.tag.ToString() == "Damage Spot") 
            {
                Collider2D changeState = (Collider2D)hitResults[i].collider;
                if (changeState.TryGetComponent(out Animator SetState)) 
                {
                    SetState.SetFloat("State", SetState.GetFloat("State") < 4 ? (SetState.GetFloat("State") + 1) : (SetState.GetFloat("State")));
                }
                    nearbySpotDetected = true;
                break;
            }
        }
        if (GameObject.FindGameObjectsWithTag("Damage Spot").Length <= maxNumberOfDamZones && !nearbySpotDetected)
        {
            GameObject damagePoint = Instantiate(damagePointPrefab, targetPosition, Quaternion.identity);
            DamageSpot damageSpot = damagePoint.GetComponent<DamageSpot>();
        }
        //Debug.Log(GameObject.FindGameObjectsWithTag("Damage Spot").Length);
    }
    private void OnDrawGizmosSelected()
    {
        // Draw a wireframe box representing the movement boundaries around the reference GameObject
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube((minPosition + maxPosition) / 2f, maxPosition - minPosition);
    }
}
