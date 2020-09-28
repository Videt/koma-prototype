using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWall : MonoBehaviour
{
    public float distance=3;
    public LayerMask grabLayer;
    public Rigidbody2D playerRigidbody;
    public Vector2 GrabVector;
    public float Force = 10;
    
    void Start()
    {
        
    }
    private void OnEnable()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, Vector2.right, Color.red,0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Input.GetAxisRaw("Horizontal"), distance,grabLayer);

        if (hit.collider != null)
        {
            OnGrab();
        }
    }

    void OnGrab()
    {
        playerRigidbody.AddForce(GrabVector * Force);
    }
}
