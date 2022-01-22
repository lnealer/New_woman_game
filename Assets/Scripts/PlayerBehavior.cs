using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float doubleJumpForce;
    public Transform groundCheck;
    public float groundCheckRadius;
    [HideInInspector]public bool doubleJump = false;
    public float runSpeed;
    public float pushForce;
    public Collider2D standingCollider;
    
    public float staminaRunDamage;
    public float staminaJumpDamage;
    [HideInInspector] public bool isPushed;
    private bool isRunning; // sprinting checker
    private Rigidbody2D rb;
    private bool facingRight = true;
    private Animator animator;
    public bool isJumping = false;
    private float moveDirection;
    public bool isGrounded;
    private int jumpCount;

    // flash these colors when damage taken
    public Color32 normalColor;
    //new Color32(255, 255, 255, 255);
    public Color32 dignityDamageColor;
    //new Color32(25, 90, 183, 255);
    public Color32 healthDamageColor;
    //new Color32(140, 39, 39, 255);


    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // for physics stuff
    private void FixedUpdate()
    {
        Grounded();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        Animate();
        StaminaReduce();
    }

    
    private void Animate()
    {
        
        if (moveDirection >0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection <0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void Move()
    {
        Run();
        Jump();
    }


    private void Run()
    {
        if (isPushed)
        {
            return;
        }
        if (isRunning & FindObjectOfType<HealthBar>().GetStamina() > 0)
        {
            rb.velocity = new Vector2(moveDirection * runSpeed, rb.velocity.y);   
        }
        else 
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    private void ProcessInput()
    {

        // get the direction of movement (left or right)
        moveDirection = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {

            if (isGrounded) 
            {
                isJumping = true;
                animator.SetBool("Grounded", false);
            }
            if (!isGrounded && doubleJump) // if the player is already jumping, check if they've double jumped yet
            {
                isJumping = true;
                doubleJump = false;
            }
        }
        if (Input.GetButtonDown("Sprint"))
        {
            // press shift to make character move faster
            isRunning = true;
        }

        if (Input.GetButtonUp("Sprint") || GetComponent<HealthBar>().health == 0)
        {
            isRunning = false;
        }
    }

    void StaminaReduce()
    {
        // check if character is sprinting and reduce stamina
        if(isRunning)
        {
            FindObjectOfType<HealthBar>().LoseHealth(staminaRunDamage);
        }
        if (isJumping)
        {
          FindObjectOfType<HealthBar>().LoseHealth(staminaJumpDamage);   
        }
    }

    public void Jump() 
    {
        float stamina = GetComponent<HealthBar>().health;
        if (isJumping)
        {
            if (stamina > 0)
            {
                if (jumpCount > 1)
                {
                    rb.AddForce(new Vector2(0f, doubleJumpForce));
                }
                else
                {
                    rb.AddForce(new Vector2(0f, jumpForce));
                }
                //GetComponent<HealthBar>().LoseHealth(5);
            }
            isJumping = false;
        }
        
    }

    

    private void Grounded()
    {

        if (isGrounded)
        {
            doubleJump = true; // reset double jump when hitting the ground
        } 

    }

    private void FlipCharacter()
    {
        // make the character flip when they change direction
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void changeDignityDamageColor()
    {
        GetComponent<SpriteRenderer>().color = dignityDamageColor;
    }

    public void changeHealthDamageColor()
    {
        GetComponent<SpriteRenderer>().color = healthDamageColor;
    }

    public void changeNormalColor()
    {
        GetComponent<SpriteRenderer>().color = normalColor;
    }

    public void pushBack()
    {
        float pushDirection = 1;
        if (facingRight)
        {
            pushDirection = -1;
        }
        isPushed=true;
        rb.AddForce(new Vector2(pushForce*pushDirection, 3f), ForceMode2D.Impulse);
    }
}
