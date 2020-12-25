using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GrapplingHook hook;

    void Start()
    {
        hook = GameObject.FindGameObjectWithTag("Hook").GetComponent<GrapplingHook>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //на данный момент работает так:
        //если тег объекта "Box", то рисует до него линию
        //если другой объект, то нет. Так что нужно добавить и другие теги
        if(collision.gameObject.tag == "Box")
        {
            hook.TargetHit(collision.gameObject);
        }
        else
        {
            hook.lineRenderer.enabled = false;
        }
        Destroy(gameObject);
    }
}
