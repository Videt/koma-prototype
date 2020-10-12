using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Event_Controller : MonoBehaviour
{   public UnityEvent MyEvent;

    
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            MyEvent.Invoke();
        }
    }
    void Update()
    {
        
    }
}
