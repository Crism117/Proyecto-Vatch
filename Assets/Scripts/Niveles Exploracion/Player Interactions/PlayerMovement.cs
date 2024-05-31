using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement
    [HideInInspector]
    public float coolDownTimer;
    public float maxCoolDownTime;
    public bool canMove;
    Vector2 targetPosition;
    public int speed = 20;
    //Direction
    Animator animator;
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        coolDownTimer -= Mathf.Clamp(Time.deltaTime, 0, maxCoolDownTime);
        canMove = PlayerClickDetection.ActionHub("Move");
        if (coolDownTimer <= 0f)
        {
            Movement();
            TurnAround();
        }
    }
    public void GamePaused() 
    {
        canMove = false;
        targetPosition = transform.position;
    }
    public void Movement()
    {
        //if player clicks on screen, make targetPosition same as mousePosition
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            targetPosition = (canMove) ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : transform.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }
    public void TurnAround()
    {
        //change direction of sprite by switching between animations with a blend tree atribute (0 face left, 1 face right)
        animator.SetFloat("Facing", transform.position.x > targetPosition.x? 0f : 1f);
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag!= "Projectile")
        canMove = false;
        targetPosition = transform.position;
    }
}
