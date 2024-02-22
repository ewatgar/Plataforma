using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator an;
    GroundDetector gd;

    public float maxHSpeed = 10, hAccel = 30, hDeccel = 30, jumpImpulse = 5;
    int dir = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        gd = GetComponentInChildren<GroundDetector>();
    }

    void FixedUpdate()
    {
        float vx = 0;
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        if (dx == 0)
        {
            vx = deceleratePlayer(vx);
        }
        else
        {
            vx = acceleratePlayer(vx, dx);
        }

        faceDirection(dx);

        an.SetFloat("Vx", Math.Abs(vx));
        rb.velocityX = vx;

        jumpPlayer(dy);
    }

    private float acceleratePlayer(float vx, float dx)
    {
        // Aceleramos en la dirección indicada por dx
        vx = rb.velocityX + dx * hAccel * Time.fixedDeltaTime;
        vx = Mathf.Clamp(vx, -maxHSpeed, maxHSpeed);
        return vx;
    }

    private float deceleratePlayer(float vx)
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

    private void faceDirection(float dx)
    {
        if (dx > 0) dir = 1;
        if (dx < 0) dir = -1;
        transform.localScale = new Vector3(dir, 1, 1);
    }

    private void jumpPlayer(float dy)
    {
        if (dy > 0 && gd.IsGrounded)
        {
            rb.AddForceY(jumpImpulse, ForceMode2D.Impulse);
        }
    }
}
