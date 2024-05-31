using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerClickDetection : MonoBehaviour
{
    public static string ClickedOn;
    public LayerMask Border;
    public void Update()
    {
        OnMouseClick();
    }
    public void OnMouseClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && (EventSystem.current.IsPointerOverGameObject()))
        {

            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Perform raycast from the mouse position
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, ~Border);

            // Check if the ray hits any collider
            if (hit.collider != null)
            {
                //When hitting an object with the component "ClickActionID" it takes the value of its actionID
                ClickedOn = hit.collider.tag.ToString();
            }
            //if player doesnt click on clickable object, actionID is set to 0 (player will move)
            else
            {
                ClickedOn = null;
                Debug.Log("Object does not have a collider!");
            }
        }
        else 
        {
            ClickedOn = null;
        }
    }
    public static bool ActionHub(string action)
    {
        bool allowAction;
        if (action == "Move")
        {
            allowAction = ClickedOn == "Movement Zone";
        }
        else allowAction = false;
        return allowAction;
    }
}
