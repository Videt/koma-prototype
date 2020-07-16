using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D hero;

    //ходьба и бег
    public float speed = 0f;
    public float walkSpeed = 10f;
    public float runSpeed = 100f;
    public float horizontalMove;

    //анимация ходьбы и бега
    /*public Animator anim;
    public bool isRunning = false;
    public bool isWalking = false;*/

    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();

        if (horizontalMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horizontalMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            speed = runSpeed;
        else
            speed = walkSpeed;

        horizontalMove = Input.GetAxis("Horizontal");
        hero.velocity = new Vector2(horizontalMove * speed, hero.velocity.y);

        /*if (speed == walkSpeed && horizontalMove != 0)
             isWalking = true;
         else if (speed == runSpeed)
             isRunning = true;

         if (isWalking == true && horizontalMove != 0)
         {
             anim.SetBool("isWalking", true);
         }
         else
         {
             anim.SetBool("isWalking", false);
         }
         if (isRunning == true)
         {
             anim.SetBool("isRunning", true);
         }
         else
         {
             anim.SetBool("isRunning", false);
         }*/
    }
}
