using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    float horizontal_value;
    Vector2 ref_velocity = Vector2.zero;

    float jumpForce = 12f;

    float moveSpeed_horizontal = 400.0f;
    bool is_jumping = false;
    bool can_jump = true;
    [Range(0, 1)] float smooth_time = 0.5f;

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

   
    void Update()
    {
        horizontal_value = Input.GetAxis("Horizontal");

        if (horizontal_value > 0) sr.flipX = false;
        else if (horizontal_value < 0) sr.flipX = true;

        if (Input.GetButtonDown("Jump") && can_jump)
        {
            is_jumping = true;
        }
    }

    void FixedUpdate()
    {
        if (is_jumping && can_jump)
        {
            is_jumping = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            can_jump = true;
        }
        Vector2 target_velocity = new Vector2(horizontal_value * moveSpeed_horizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp(rb.velocity, target_velocity, ref ref_velocity, 0.05f);
    }
}

