using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float walkSpeed = 5f;
    [SerializeField] public float runSpeed = 10f;
    public bool isGrounded = false;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask whatIsGround;
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

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
        controls.Gameplay.Move.performed += ctx => isWalking = true;
        controls.Gameplay.Move.canceled += ctx => isWalking = false;

        controls.Gameplay.Run.performed += ctx => speed = runSpeed;
        controls.Gameplay.Run.canceled += ctx => speed = walkSpeed;
        controls.Gameplay.Run.performed += ctx => isRunning=true;
        controls.Gameplay.Run.canceled += ctx => isRunning=false;

        controls.Gameplay.Jump.performed += ctx => Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
    void Jump()
    {
        if (isGrounded)
        {
            hero.AddForce(new Vector2(0, jumpForce));
            anim.SetTrigger("Jump");
        }
            
    }
}
