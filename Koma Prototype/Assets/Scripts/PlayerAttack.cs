using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;

    public Animator anim;

    void Update()
    {
        if (timeBtwAttack <= 0)
        { if (Input.GetMouseButton(0))
            {
                anim.SetTrigger("attack");
                timeBtwAttack = startTimeBtwAttack;
            }
            
        }
        

    
    else {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
 
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            //  enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("attack");

        }
    }
}
