using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogatka : MonoBehaviour
{
    private Vector3 startPos = Vector3.zero;
    public Vector3 initPos = Vector3.zero;
    private Vector3 endPos;
    private Vector3 forceAtPlayer;
    private Rigidbody2D rb;
    public float forceFactor;

    public GameObject trajectoryDot;
    public GameObject[] trajectoryDots;
    private bool isPressed;
    private Vector3 mousePos;

    public int number;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trajectoryDots = new GameObject[number];
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        isPressed = true;
        //rb.isKinematic=true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        //rb.isKinematic = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // start
        {
            startPos = gameObject.transform.position;
            for (int i =0; i< number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }
        }
        if (Input.GetMouseButton(0))  //drag
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPos = new Vector3(mousePos.x, mousePos.y, 0f);
            transform.position = endPos;
            forceAtPlayer = endPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i  * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0)) //leave
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y *forceFactor);
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            gameObject.transform.position = initPos;
        }

        
    }
    private Vector2 calculatePosition(float elapsedTime)
    {
        return new Vector2(endPos.x, endPos.y) +
             new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime +
             0.5f * Physics2D.gravity * elapsedTime * elapsedTime;
    }
}
