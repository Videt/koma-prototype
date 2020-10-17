using UnityEngine;

public class Test_Movement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    [Range(0, 15)]
    public int maxMovementSpeed = 5;
    [Range(0, 500)]
    public int maxJumpSpeed = 100;

    public int maxJumps;
    public int currentJump;
    public float groundDis;

    public LayerMask groundLayer;

    public bool isGround;
    public Transform groundCheckPos;

    public Vector2 axis;

    private void OnEnable()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
 
        Vector2 playerVelocity = playerRigidbody2D.velocity;
        playerRigidbody2D.velocity = new Vector2(maxMovementSpeed * axis.x, playerVelocity.y);

    }
    private void Update()
    {
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        flip();
        isGround = Physics2D.OverlapCircle(groundCheckPos.position, groundDis,groundLayer);

        if (isGround)
        {
            currentJump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGround || maxJumps > currentJump))
        {    
                Vector2 playerVelocity = playerRigidbody2D.velocity;
                Debug.Log("Jump");
                currentJump++;
                playerRigidbody2D.velocity = new Vector2(playerVelocity.x, maxJumpSpeed);
                isGround = false;          
        }
    }

    void flip()
    {
        if (axis.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);        
        }
        else if (axis.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);        
        }
    }
}

  
