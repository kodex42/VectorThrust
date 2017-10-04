using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private const float MAX_SPEED = 3;

    private Rigidbody2D rb2d;
    private Animator animator;
    public GameObject dirIndicator;

    // Stats
    public const int MAX_HEALTH = 100;
    public int curHealth;

    // Movement
    public float delayBeforeDoubleJump;
    public float speed;
    public float jumpPower;
    public float superJumpPower;
    public bool canDoubleJump;
    public bool grounded;
    public bool inStasis;
    public bool forceOutStasis;
    private Vector2 storedVelocity;

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        rb2d.freezeRotation = true;

        curHealth = MAX_HEALTH;
	}
	
	// Update is called once per frame
	void Update () {
        if (!PauseMenu.isPaused) {
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

            // Make sure the character is facing the correct way when moving
            if (Input.GetAxis("Horizontal") < 0f)
                transform.localScale = new Vector3(-1, 1, 1);

            if (Input.GetAxis("Horizontal") > 0f)
                transform.localScale = new Vector3(1, 1, 1);

            // Jump handler
            if (Input.GetButtonDown("Jump") && (grounded || canDoubleJump || inStasis))
                Jump();

            if (curHealth > MAX_HEALTH)
                curHealth = MAX_HEALTH;

            if (curHealth <= 0)
                die();

            inStasis = Input.GetAxis("Stasis") > 0 && !grounded;

            if (inStasis && !forceOutStasis)
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            else
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            dirIndicator.SetActive(inStasis && !forceOutStasis);

            if (forceOutStasis)
                rb2d.gravityScale = 0;
            else
                rb2d.gravityScale = 1;
        }
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

        // Moving the Player
        if ((!inStasis && !forceOutStasis))
            rb2d.AddForce(Vector2.right * hSpeed);

        // Limiting Speed
        if (rb2d.velocity.x > MAX_SPEED && !forceOutStasis)
            rb2d.velocity = new Vector2(MAX_SPEED, rb2d.velocity.y);

        if (rb2d.velocity.x < -MAX_SPEED && !forceOutStasis)
            rb2d.velocity = new Vector2(-MAX_SPEED, rb2d.velocity.y);
    }

    private void Jump() {
        if (!inStasis || forceOutStasis) {
            if (grounded)
                canDoubleJump = true;
            if (canDoubleJump) {
                canDoubleJump = false;
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            }

            if (!forceOutStasis)
                rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
        }
        else {
            forceOutStasis = true;
            DirIndicatorFollow indicatorScript = dirIndicator.GetComponent<DirIndicatorFollow>();
            rb2d.velocity = new Vector2(0, 0);
            rb2d.velocity = new Vector2(indicatorScript.angleX * superJumpPower, -indicatorScript.angleY * superJumpPower);
        }
    }

    public void dealDamage(int damage) {
        curHealth -= damage;
    }

    void die() {
        SceneManager.LoadScene(0);
    }
}
