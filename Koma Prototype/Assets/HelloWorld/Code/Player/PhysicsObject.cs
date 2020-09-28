using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    protected bool grounded;
    protected Vector2 groundNormal;
    public float gravityModifer = 1f;
    public float minGroundNormalY = .65f;
    protected Rigidbody2D playerRigidbody;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter2D;
    protected const float minMoveDistance = 0.001f;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected const float shellRadius = 0.01f;
    protected Vector2 targetVelocity;
    Animator anim;
    private void OnEnable()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter2D.useTriggers = false;
        contactFilter2D.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter2D.useLayerMask = true;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
        
    }
    protected virtual void ComputeVelocity()
    {

    }
    private void FixedUpdate()
    {
        velocity += gravityModifer * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move,true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance> minMoveDistance)
        {

            int count = playerRigidbody.Cast(move, contactFilter2D,hitBuffer,distance+shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i< count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                float modiferDistance = hitBufferList[i].distance - shellRadius;
                distance = modiferDistance < distance ? modiferDistance : distance;
            }

        }

        playerRigidbody.position = playerRigidbody.position + move.normalized * distance;
        
    }
}
