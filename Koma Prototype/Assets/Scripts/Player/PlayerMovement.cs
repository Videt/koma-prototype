using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerMovement : MonoBehaviour
{
    //Движение
    PlayerControls controls;
    Vector2 move;
    float speed = 5f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float climbSpeed = 4f;

    //Прыжок
    public float jumpForce = 300f;
    public bool isGrounded = false;
    public bool canWallJump = false;
    public float horizontalJumpForce = 4000f;

    Rigidbody2D hero;
    Transform groundCheck;
    float groundRadius = 0.1f;
    LayerMask whatIsGround;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public bool isGrounded = false;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Transform Effect;
    public Animator anim;
    public bool isRunning;
    public bool isWalking;

    private PlayerControls controls;
    private Vector2 move;
    private float speed = 5f;
    private float jumpForce = 300f;
    private Rigidbody2D hero;
    private float groundRadius = 1f;

    void Awake()
    {
        hero = GetComponent<Rigidbody2D>();

        controls = new PlayerControls();

        //Ходьба
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Move.performed += ctx => isWalking = true;
        controls.Gameplay.Move.canceled += ctx => isWalking = false;

        //Бег
        controls.Gameplay.Run.performed += ctx => speed = runSpeed;
        controls.Gameplay.Run.canceled += ctx => speed = walkSpeed;
        controls.Gameplay.Run.performed += ctx => isRunning = true;
        controls.Gameplay.Run.canceled += ctx => isRunning = false;

        //Прыжок
        controls.Gameplay.Jump.performed += ctx => Jump();

        //Взобраться на стену
        controls.Gameplay.Climb.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Climb.canceled += ctx => move = Vector2.zero;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Move();
        Climb();
        if (isWalking == true)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        if (isRunning==true)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * speed;
        transform.Translate(m, Space.World);
        var x = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if (!x && isGrounded)
        {
            Instantiate(Effect, groundCheck.position,Quaternion.identity);
        }
            if (move.x<0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (move.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canWallJump = false;
        }
    }
    void Move()
    {
        //Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * speed;
        //transform.Translate(m, Space.World);
        float h = move.x;
        hero.velocity = new Vector2(h * speed, hero.velocity.y);
        if (h < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (h > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Jump()
    {
        if (canWallJump && !isGrounded)
            hero.AddForce(new Vector2(horizontalJumpForce, jumpForce * 25));
        else if (isGrounded)
        if (isGrounded)
        {
            hero.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jump");
        }
            
    }
    void Climb()
    {
        if (canWallJump && !isGrounded)
        {
            float v = move.y;
            hero.velocity = new Vector2(hero.velocity.x, v * climbSpeed);
        }
    }
}
