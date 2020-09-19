using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Event_Controller : MonoBehaviour
{
    public UnityEvent EventOnEnter;
    public UnityEvent EventOnClick;
    public KeyCode EventKey;


    
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventOnEnter.Invoke();         
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(EventKey))
            {
                EventOnClick.Invoke();
                Debug.Log("LLOLL");
            }
        }
    }
    void Update()
    {
        
    }
}
