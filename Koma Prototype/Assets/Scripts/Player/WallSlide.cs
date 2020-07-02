using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    PlayerMovement player;
    public float distance = 2f;
    public float speed = 2f;
    public float Axis;
    public Transform Point;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(Point.transform.position, -transform.right, distance);
        Debug.DrawLine(transform.position, hit.point, Color.red);

        Axis = Input.GetAxisRaw("Vertical");
        if (player.isGrounded == false && hit.collider != null)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < speed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed*Axis);
            }
        }
    }
}
