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

    PlayerPushAndPull playerPushAndPull;//нужно, чтобы определить потом, держит/двигает ли игрок предмет

    //переменные для взбирания на уступы
    public float rayDistance = 0.38f;
    private RaycastHit2D hitLedgeUp;
    private RaycastHit2D hitLedgeDown;
    private Vector3 climbPos;
    private float defaultGravityScale;
    public Transform hitLedgeUpPos;
    public Transform hitLedgeDownPos;
    private Vector2 rayDirection;
    public float ledgeClimbDistance = 0.85f;
    public float ledgeClimbHeight = 1.5f;

    private bool isUmbrellaOpened = false;
    private void OnEnable()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        defaultGravityScale = playerRigidbody2D.gravityScale;
        playerPushAndPull = GetComponent<PlayerPushAndPull>();
    }

    void FixedUpdate()
    {
        Vector2 playerVelocity = playerRigidbody2D.velocity;
        playerRigidbody2D.velocity = new Vector2((maxMovementSpeed + (maxRunSpeed * runAxis)) * axis.x , playerVelocity.y);
       
        //Animator
        player_Animator.SetInteger("axisX",Mathf.Abs(Mathf.CeilToInt(playerRigidbody2D.velocity.x)));
        player_Animator.SetInteger("axisY", Mathf.CeilToInt(playerRigidbody2D.velocity.y));
        //

        LedgeClimb();

        Umbrella();
    }
    private void Update()
    {
        //Animator
        player_Animator.SetBool("isGround", isGround);
        //

        runAxis = Input.GetAxis("LeftShifh");
        axis.x = Input.GetAxis("Horizontal");
        axis.y = Input.GetAxis("Vertical");

        Flip();
        isGround = Physics2D.OverlapCircle(groundCheckPos.position, groundDis, groundLayer);

        if (isGround)
        {
            currentJump = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (isGround || maxJumps > currentJump))
        {
            Jump();
            
        }

        #region Инпут для зонтика
        //если нажать на V, то откроет зонт (замедлит падение)
        if (Input.GetKeyDown(KeyCode.V))
        {
            isUmbrellaOpened = true;
        }
        //если приземлиться или нажать B, то зонтик закроется
        if (Input.GetKeyDown(KeyCode.B) || isGround == true)
        {
            isUmbrellaOpened = false;
        }
        #endregion
    }

    void Flip()
    {
        //если не держит/двигает объект, то можно повернуть спрайт
        if (axis.x < 0 && playerPushAndPull.isHoldingObject == false)
        {
            puppet2d.flip = true;
        }
        else if (axis.x > 0 && playerPushAndPull.isHoldingObject == false)
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

    void Umbrella()
    {
        //если нажать на V, то откроет зонт (замедлит падение)
        if (isUmbrellaOpened == true)
        {
            playerRigidbody2D.drag = 10;
        }
        //если приземлиться или нажать B, то зонтик закроется
        if (isUmbrellaOpened == false)
        {
            playerRigidbody2D.drag = 0;
        }
    }

    void LedgeClimb()
    {
        if (axis.x < 0)
        {
            rayDirection = Vector2.left;
        }
        else if (axis.x > 0)
        {
            rayDirection = Vector2.right;
        }
        Physics2D.queriesStartInColliders = false;
        hitLedgeUp = Physics2D.Raycast(hitLedgeUpPos.position, rayDirection * transform.localScale.x, rayDistance);
        hitLedgeDown = Physics2D.Raycast(hitLedgeDownPos.position, rayDirection * transform.localScale.x, rayDistance);

        //если сверху достаточно места, то персонаж перемещается на верхний уступ
        //числа подбирал вручную
        if (hitLedgeDown.collider != null && hitLedgeUp.collider == null)
        {
            playerRigidbody2D.velocity = new Vector3(0, 0, 0);
            playerRigidbody2D.gravityScale = 0;
            climbPos = GetComponent<Transform>().position;

            //проверяет, в какую сторону смотрит персонаж, чтобы правильно переместиться
            if(puppet2d.flip == false)
            { playerRigidbody2D.position = new Vector3(climbPos.x + ledgeClimbDistance, climbPos.y + ledgeClimbHeight, 0); }
            else
            { playerRigidbody2D.position = new Vector3(climbPos.x - ledgeClimbDistance, climbPos.y + ledgeClimbHeight, 0); }
        }
        playerRigidbody2D.gravityScale = defaultGravityScale;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(hitLedgeUpPos.position, rayDirection * rayDistance);
        Gizmos.DrawRay(hitLedgeDownPos.position, rayDirection * rayDistance);
    }
}

  
