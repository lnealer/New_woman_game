using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public GameObject player;
    private PlayerBehavior playerBehavior;
    private bool isGrounded;
    private Animator animator;
    public LayerMask groundObjects;



    void Awake()
    {
        playerBehavior = player.GetComponent<PlayerBehavior>();
        animator =  player.GetComponent<Animator>();

        isGrounded = Physics2D.OverlapCircle(playerBehavior.groundCheck.position, 
                playerBehavior.groundCheckRadius, groundObjects);
        animator.SetBool("Grounded", isGrounded);
        playerBehavior.isGrounded = isGrounded;
    }


  void OnCollisionStay2D(Collision2D collision)
  {
      if (collision.gameObject.tag == "Ground")
      {
          isGrounded = true;
          playerBehavior.isGrounded = true;
          animator.SetBool("Grounded", true);
      }
  }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            playerBehavior.isPushed = false;
            playerBehavior.isGrounded = true;
            animator.SetBool("Grounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Ground")
        {
            animator.SetBool("Grounded", isGrounded);    
            isGrounded = false;
            playerBehavior.isGrounded = false;
            animator.SetBool("Grounded", false);
        }

    }

}

