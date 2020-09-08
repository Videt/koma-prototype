using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Contoller : MonoBehaviour
{
    public GameObject Icon;
    public LampLight lamp;
    public float Timer;
    float timer;
    public float distance;
    public bool hold;
    RaycastHit2D hit;
    public Transform holdPoint;
    public float ThrowObj;
    public GameObject Camera;
    public float minZoom;
    public float maxZoom;
    public int CamZoom;
    void Start()
    {

        timer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.right * distance, Color.red);
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hold)
            {
                Physics2D.queriesStartInColliders = false;
                Debug.DrawRay(holdPoint.transform.position, transform.right * distance, Color.red);
                hit = Physics2D.Raycast(transform.position, transform.right, distance);
                
                if (hit.collider !=null && hit.collider.CompareTag("Object") && hit.collider != null)
                {
                    hold = true;
                }
            }
            else
            {
                hold = false;
                if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
                {
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 1) * ThrowObj;
                }
            }
        }

        if (hold )
            
        {
            hit.collider.gameObject.transform.position = holdPoint.position;

            if (holdPoint.position.x>transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.transform.localRotation = Quaternion.Euler(0, 180, 0);

            }
            else if (holdPoint.position.x < transform.position.x && hold == true)
            {
                hit.collider.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);

            }


        }
    }
 /*   private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(holdPoint.transform.position, holdPoint.transform.position + Vector3.right  * distance);
    }*/
    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Lamp"))
        {
            Icon.SetActive(true);
            lamp = collision.GetComponent<LampLight>();
        }
        if (collision.gameObject.CompareTag("ZoomMin"))
        {
            Camera.GetComponent<Camera>().orthographicSize = Mathf.Clamp(Camera.GetComponent<Camera>().orthographicSize-1, minZoom, maxZoom);
        }
        if (collision.gameObject.CompareTag("ZoomMax"))
        {
            Camera.GetComponent<Camera>().orthographicSize = Mathf.Clamp(Camera.GetComponent<Camera>().orthographicSize + 1, minZoom, maxZoom);
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        
        if (Input.GetKey(KeyCode.E) && timer<=0)
        {
            if (lamp.On == true)
            {
                lamp.Light.SetActive(false);
                lamp.On = false;
                timer = Timer;
            }
            else
            {
                lamp.Light.SetActive(true);
                lamp.On = true;
                timer = Timer;
            }
        }
    }
   
        public void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("Lamp"))
        {
            Icon.SetActive(false);
        }
    }

}
