using UnityEngine;

public class Test_Movement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D;
    [Range(0, 15)]
    public int maxMovementSpeed = 5;
    [Range(0, 500)]
    public int maxJumpSpeed = 100;

    public int countJumps;
    public int count;
    public float groundDis;

    public LayerMask groundLayer;

    public bool isGround;
    public Transform groundCheckPos;

    private void OnEnable()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
 
        Vector2 playerVelocity = playerRigidbody2D.velocity;
        playerRigidbody2D.velocity = new Vector2(maxMovementSpeed * Input.GetAxis("Horizontal"), playerVelocity.y);

    }
    private void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPos.position, groundDis,groundLayer);

        if (isGround)
        {
            count = countJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround || (!isGround && count > 0))
            {
                Vector2 playerVelocity = playerRigidbody2D.velocity;
                Debug.Log("Jump");
                count--;
                playerRigidbody2D.velocity = new Vector2(playerVelocity.x, maxJumpSpeed);
                isGround = false;
            }


        }
    }
}

  
