using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    float speed = 5f;
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;

    float jumpForce = 300f;

    Rigidbody2D hero;
    bool isGrounded = false;
    [SerializeField] Transform groundCheck;
    float groundRadius = 0.1f;
    [SerializeField] LayerMask whatIsGround;
    void Awake()
    {
        hero = GetComponent<Rigidbody2D>();

        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Run.performed += ctx => speed = runSpeed;
        controls.Gameplay.Run.canceled += ctx => speed = walkSpeed;

        controls.Gameplay.Jump.performed += ctx => Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * speed;
        transform.Translate(m, Space.World);
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
            hero.AddForce(new Vector2(0, jumpForce));
    }
}
