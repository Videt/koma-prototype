using UnityEngine;
using Puppet2D;

public class Test_Movement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    [Range(0, 15)]
    public int maxMovementSpeed = 5;
    [Range(0, 15)]
    public int maxRunSpeed;
    [Range(0, 500)]
    public int maxJumpSpeed = 100;

    public int maxJumps;
    public int currentJump;
    public float groundDis;

    public LayerMask groundLayer;

    public bool isGround;
    public Transform groundCheckPos;

    public float runAxis;
    public Vector2 axis;
    public Puppet2D_GlobalControl puppet2d;
    public Animator player_Animator;

    private void OnEnable()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {

        Vector2 playerVelocity = playerRigidbody2D.velocity;
        playerRigidbody2D.velocity = new Vector2((maxMovementSpeed + (maxRunSpeed * runAxis)) * axis.x , playerVelocity.y);
       
        //Animator
        player_Animator.SetInteger("axisX",Mathf.Abs(Mathf.CeilToInt(playerRigidbody2D.velocity.x)));
        player_Animator.SetInteger("axisY", Mathf.CeilToInt(playerRigidbody2D.velocity.y));
        //

    }
    private void Update()
    {
        //Animator
        player_Animator.SetBool("isGround", isGround);
        //

        runAxis = Input.GetAxis("LeftShifh");
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        flip();
        isGround = Physics2D.OverlapCircle(groundCheckPos.position, groundDis, groundLayer);

        if (isGround)
        {
            currentJump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGround || maxJumps > currentJump))
        {
            Jump();
            
        }
    }

    void flip()
    {
        if (axis.x < 0)
        {
            puppet2d.flip = true;
        }
        else if (axis.x > 0)
        {
            puppet2d.flip = false;
        }

        /*if (axis.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        else if (axis.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }*/
        
    }
    void Jump()
    {
        Vector2 playerVelocity = playerRigidbody2D.velocity;
        Debug.Log("Jump");
        currentJump++;
        playerRigidbody2D.velocity = new Vector2(playerVelocity.x, maxJumpSpeed);
        isGround = false;
    }
}

  
