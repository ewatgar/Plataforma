using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    GroundDetector gd;

    [SerializeField] int dir = 1;
    public float maxSpeed = 10, hAccel = 30, hDeccel = 30, jumpImpulse = 11;
    public float maxSpeedWater = 5;
    public float jumpDecreaseWater = 40;
    public bool bIsJumping = false;
    public bool bIsInWater = false;
    public bool canMove = true;
    float vx = 0;
    float vy = 0;
    float dx = 0;
    float dy = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        gd = GetComponentInChildren<GroundDetector>();
    }

    void FixedUpdate()
    {
        dx = Input.GetAxis("Horizontal");
        dy = Input.GetAxis("Vertical");

        if (canMove)
        {
            Movement();
        }

        InitAnimatorParameters();
    }

    private void Movement()
    {
        FaceDirection(dx);

        if (dx == 0)
        {
            vx = Decelerate(vx);
        }
        else
        {
            vx = Accelerate(vx, dx);
        }
        rb.velocityX = vx;

        Jump(dy);
    }

    private float Accelerate(float vx, float dx)
    {
        // Aceleramos en la dirección indicada por dx
        vx = rb.velocityX + dx * hAccel * Time.fixedDeltaTime;
        if (bIsInWater)
        {
            vx = Mathf.Clamp(vx, -maxSpeedWater, maxSpeedWater);
        }
        else
        {
            vx = Mathf.Clamp(vx, -maxSpeed, maxSpeed);
        }
        return vx;
    }

    private float Decelerate(float vx)
    {
        float delta;
        if (rb.velocityY != 0)
        {
            delta = hDeccel * 0.25f * Time.fixedDeltaTime;
        }
        else
        {
            delta = hDeccel * Time.fixedDeltaTime;
        }

        // Deceleramos al personaje en la dirección contraria a su movimiento
        if (rb.velocityX > 0)
        {
            // Me muevo hacia la derecha
            vx = rb.velocityX - delta;
            if (vx < 0) vx = 0;
        }
        else if (rb.velocityX < 0)
        {
            // Me muevo hacia la izquierda
            vx = rb.velocityX + delta;
            if (vx > 0) vx = 0;
        }
        return vx;
    }

    private void FaceDirection(float dx)
    {
        if (dx > 0) dir = 1;
        if (dx < 0) dir = -1;
        transform.localScale = new Vector3(dir, 1, 1);
    }

    private void Jump(float dy)
    {
        if (dy > 0 && bIsInWater)
        {
            rb.velocityY = 0;
            rb.AddForceY(jumpImpulse / jumpDecreaseWater, ForceMode2D.Impulse);
            bIsJumping = true;
        }
        else if (dy > 0 && gd.IsGrounded)
        {
            rb.velocityY = 0;
            rb.AddForceY(jumpImpulse, ForceMode2D.Impulse);
            bIsJumping = true;
        }
        else if (vy == 0 && gd.IsGrounded)
        {
            bIsJumping = false;
        }

        CalculateJumpVelocity();
    }

    private void CalculateJumpVelocity()
    {
        Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);
        vy = localVelocity.y;
    }

    private void InitAnimatorParameters()
    {
        an.SetFloat("Vx", Math.Abs(vx));
        an.SetFloat("Vy", Math.Abs(vy));
        an.SetBool("IsJumping", bIsJumping);
        an.SetBool("IsInWater", bIsInWater);
    }
}
