using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Slide : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2d;
    public Transform checkWall;
    public float wallSlideSpeed = 2;
    public float checkWallDis;
    public LayerMask wallLayer;
    private bool canSlide;
    private bool collideWall;
    private float x, y;
    public float canGrabtime = 5, reloadGrabtime = 5;

    public float Ystalost;

    void OnEnable()
    {
        canSlide = true;
        playerRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
         x = Input.GetAxis("Horizontal");
         y = Input.GetAxis("Vertical");

        collideWall = Physics2D.OverlapCircle(checkWall.position, checkWallDis, wallLayer);
        if (collideWall && canSlide)
        {
            Vector2 playerVelocity = playerRigidbody2d.velocity;
            if (x != 0)
            {
                if (Ystalost > canGrabtime)
                {
                    StartCoroutine(GrabWall());
                    Debug.Log("Я устал!");
                }
                playerRigidbody2d.velocity = new Vector2(playerVelocity.x, wallSlideSpeed);
                Ystalost += 1 * Time.deltaTime;
            }
        }
        else
        {
            if (Ystalost > 0)
            {
                Ystalost -= 1 * Time.deltaTime;
            }
        }
    }
    IEnumerator GrabWall()
    {
        yield return new WaitForSeconds(canGrabtime);
        canSlide = false;
        StartCoroutine(ReloadGrab());
    }
    IEnumerator ReloadGrab()
    {
        yield return new WaitForSeconds(reloadGrabtime);
        canSlide = true;
    }
}
