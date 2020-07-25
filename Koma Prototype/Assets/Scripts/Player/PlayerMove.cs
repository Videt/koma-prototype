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
    // проверка стоит ли перс на земле
    public bool isGrounded;
    //анимация ходьбы и бега
    public Animator anim;
    public bool isRunning = false;
    public bool isWalking = false;
    public bool faceright = true;
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
            faceright = false;
        }
        else if (horizontalMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            faceright = true;
        }
    }
    public void Update()
    {
        if (isWalking == false && isRunning == false)
        {
            anim.SetBool("Idle", true);
        }
        else
        {
            anim.SetBool("Idle", false);
        }
        //----------------------
        if (isWalking == true)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        //----------------------
        if (isRunning == true)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    void Move()
    {

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            if (horizontalMove != 0)
            {
                isRunning = true;
                isWalking = false;
            }
            else
            {
                isWalking = false;
                isRunning = false;
            }
        }
        else
        {
            speed = walkSpeed;
            if (horizontalMove != 0)
            {
                isWalking = true;
                isRunning = false;
            }
            else
            {
                isWalking = false;
                isRunning = false;
            }
        }

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
