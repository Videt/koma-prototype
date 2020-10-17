using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Slide : MonoBehaviour
{
    public Transform checkWall;
    public float wallSlideSpeed = 2;
    public float checkWallDis;
    public LayerMask wallLayer;
    public float reloadGrabtime = 5;
    public float maxStamina;
    public float current_stamina;
    public bool isGrab;
    private Rigidbody2D playerRigidbody2d;
    public bool canGrab;
    private bool collideWall;
    private float x, y;
    //обображение параметров для разрабов
    public Slider stamina_slider;
    public Camera cam;
    public Vector2 offcet;

    void OnEnable()
    {
        canGrab = true;
        playerRigidbody2d = gameObject.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
         x = Input.GetAxis("Horizontal");
         y = Input.GetAxis("Vertical");
        DrawParameters();
        collideWall = Physics2D.OverlapCircle(checkWall.position, checkWallDis, wallLayer); // Если коснулись стены то true

        if (collideWall && canGrab &&x != 0) // если коснулись стены и можем ползти
        {
            isGrab = true;
            Vector2 playerVelocity = playerRigidbody2d.velocity; // записываем велосити игрока в отдельную переменную

            if (x != 0) // если идем в бок
            {
                if (current_stamina > maxStamina) // если стамина превышает максимальную стамину, мы устаём.
                {
                    StartCoroutine(ReloadGrab());
                    Debug.Log("Я устал!");
                }
                playerRigidbody2d.velocity = new Vector2(playerVelocity.x, wallSlideSpeed); // движение по стене
                current_stamina += 1 * Time.deltaTime; // тратим силу на то чтобы ползти
            }          
        }
        else
        {
            isGrab = false;
            if (current_stamina > 0) //отдых
            {
                current_stamina -= 1 * Time.deltaTime;
            }
        }
    }
  
    IEnumerator ReloadGrab() // если устали то ждем.
    {
        canGrab = false;
        yield return new WaitForSeconds(reloadGrabtime);
        canGrab = true;
    }
    void DrawParameters()
    {    
        stamina_slider.maxValue = maxStamina;
        stamina_slider.value = current_stamina;
        Vector2 pos = cam.WorldToScreenPoint(transform.position);
        stamina_slider.GetComponent<RectTransform>().position =( pos+ offcet);
    }
}
