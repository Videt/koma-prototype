using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D hero;

    public float jumpForce = 2000f;
    public bool isJumping;
    public bool isGrounded = false;
    private float groundRadius = 0.5f;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    //Скольжение по стене и прыжок от нее
    public bool isTouchingFront;
    public Transform frontCheck;
    private bool isWallSliding;
    public float wallSlidingSpeed;
    public LayerMask whatIsWall;
    private float wallRadius = 0.5f;

    //Прыжок от стены
    private bool isWallJumping;
    public float xWallForce; //сила прыжка по горизонтали
    public float yWallForce; //сила прыжка по верикали
    public float wallJumpTime; //в течение какого времени будут применяться силы xWallForce и yWallForce
    private float directionOfJump;

    public Transform Effect;
    //public Animator anim;
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        directionOfJump = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Jump();
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, wallRadius, whatIsWall);
        WallSlide();
        WallJump();
    }

    void Jump()
    {
        isJumping = Input.GetButton("Jump");
        if (isGrounded && isJumping)
        {
            hero.AddForce(new Vector2(0, jumpForce));
            //anim.SetTrigger("Jump");
        }
    }
    void WallSlide()
    {
        if (isTouchingFront == true && isGrounded == false && directionOfJump != 0)
        {
            isWallSliding = true;
            //anim.SetTrigger("WallSlide");
        }
        else
        {
            isWallSliding = false;
        }
        if (isWallSliding)
        {
            hero.velocity = new Vector2(hero.velocity.x, Mathf.Clamp(hero.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
    }
    void WallJump()
    {
        if (isJumping && isWallSliding)
        {
            isWallJumping = true;
            Invoke(nameof(SetWallJumpingToFalse), wallJumpTime);
        }

        if (isWallJumping)
        {
            //anim.SetTrigger("WallJump");
            hero.velocity = new Vector2(xWallForce * -directionOfJump, yWallForce);
        }
    }
    void SetWallJumpingToFalse()
    {
        isWallJumping = false;
    }
}
