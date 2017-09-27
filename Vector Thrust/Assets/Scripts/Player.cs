﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private const float MAX_SPEED = 3;

    private Rigidbody2D rb2d;
    private Animator animator;

    public float delayBeforeDoubleJump;
    public float speed = 50f;
    public float jumpPower = 1;
    public bool canDoubleJump;
    public bool grounded;

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rb2d.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        // Make sure the character is facing the correct way when moving
        if (Input.GetAxis("Horizontal") < 0f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetAxis("Horizontal") > 0f)
            transform.localScale = new Vector3(1, 1, 1);

        // Jump handler
        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void FixedUpdate() {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;

        float hSpeed = Input.GetAxis("Horizontal") * speed;

        // Ease velocity for fake friction
        if (grounded) {
            easeVelocity.x *= 0.75f;
            rb2d.velocity = easeVelocity;
        }
        else {
            easeVelocity.x *= 0.90f;
            rb2d.velocity = easeVelocity;
        }

        // Moving the Player
        rb2d.AddForce(Vector2.right * hSpeed);

        // Limiting Speed
        if (rb2d.velocity.x > MAX_SPEED)
            rb2d.velocity = new Vector2(MAX_SPEED, rb2d.velocity.y);

        if (rb2d.velocity.x < -MAX_SPEED)
            rb2d.velocity = new Vector2(-MAX_SPEED, rb2d.velocity.y);
    }

    private void Jump() {
        if (grounded) {
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
        }
        if (canDoubleJump) {
            canDoubleJump = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }

    }

    void EnableDoubleJump() {
        canDoubleJump = true;
    }
}