using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject
{
    public float jumpTakeSpeed = 7;
    public float maxSpeed = 7;
    public float maxRunSpeed;
    private SpriteRenderer spriteRenderer;
    public bool flipSprite;
    public bool run;
  //  public bool run;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ComputeVelocity()
    {
        #region В какую сторону идём
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxisRaw("Horizontal");
        #endregion 

        #region Прыжок
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeSpeed;
        }
        #endregion

        #region Проверка стороны поворота
        // 
        if (move.x > 0.01f)
        {
            flipSprite = true;
        }
        else if (move.x < -0.01f)
        {
            flipSprite = false;
        }
        #endregion

        #region Поворот

        if (flipSprite)
        {

            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.LeftShift) && !run)
        {
            run = true;
            maxSpeed += maxRunSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            run = false;
            maxSpeed -= maxRunSpeed;
        }


       
        targetVelocity = move * maxSpeed; // Идем 
    }      
}
