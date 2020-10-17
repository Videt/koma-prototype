using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Camera : MonoBehaviour
{
    private Collider2D enemy;
    public Transform player;
    public LayerMask enemyMask;
    public Vector3 offcet;
    public float speed;
    public float dis;
    public float dis2;


    void Start()
    {
       
    }

   
    
    void Update()
    {

    enemy = Physics2D.OverlapCircle(player.position,dis, enemyMask);
      

        if (enemy != null)
        {
            /*  Vector2 pos;
              Vector3 newPos;         
              pos =enemy.transform.position - player.transform.position;
              newPos = Vector2.MoveTowards(transform.position, pos, speed);
              transform.position = newPos + offcet;     
              */
              Vector3 newPos = Vector3.Slerp(player.position, enemy.transform.position, .5f) + offcet;
              transform.position = Vector3.MoveTowards(transform.position, newPos, speed);
        }
       
   


    }









    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.position, dis);    
    }
}

