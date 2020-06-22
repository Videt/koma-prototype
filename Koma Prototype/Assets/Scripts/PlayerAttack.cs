using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;

    public Animator anim;

    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Hit.performed += ctx => OnAttack();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisavle()
    {
        controls.Gameplay.Disable();
    }
    void Update()
    {
            timeBtwAttack -= Time.deltaTime;
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
 
    }
    public void OnAttack()
    {
        if (timeBtwAttack <= 0)
        {
         anim.SetTrigger("attack");
         timeBtwAttack = startTimeBtwAttack;
        }
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            //  enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("attack");

        }
    }
}
