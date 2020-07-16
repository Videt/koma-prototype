using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D hero;

    public float jumpForce = 1000f;
    public bool isJumping;
    public bool isGrounded = false;
    float groundRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public Transform Effect;
    //public Animator anim;
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Jump();
    }

    void Jump()
    {
        isJumping = Input.GetButton("Jump");
        if (isGrounded && isJumping == true)
        {
            hero.AddForce(new Vector2(0, jumpForce));
            //anim.SetTrigger("Jump");
        }
    }
}
