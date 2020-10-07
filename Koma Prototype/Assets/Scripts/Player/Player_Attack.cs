using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform hitPoint;
    public float radius;
    public float distance;
    public RaycastHit2D[] result = new RaycastHit2D[1];
    public LayerMask Enemy;
    public Animator anim;
   
    void Start()
    {
       
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.position, radius);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        result = Physics2D.CircleCastAll(hitPoint.position, radius, hitPoint.forward,distance,Enemy);

        for (int i = 0; i < result.Length; i++)
        {
            Debug.Log(result[i].collider.name);
        }
        anim.SetTrigger("Attack");




    }
}
