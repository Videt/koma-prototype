using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public Transform jumpAttackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage = 1;
    public PlayerMovement playerMovement;
    public Animator anim;

    PlayerControls controls;
    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Attack.performed += ctx => OnAttack();
        controls.Gameplay.PowerfulAttack.performed += ctx => OnPowerfulAttack();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void Update()
    {
            timeBtwAttack -= Time.deltaTime; 
    }
    private void OnDrawGizmosSelected()         // отрисовка в Gizmos  области, которая наносит урон
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(jumpAttackPos.position, attackRange);
    }
    private void OnAttack()
    {
        if (timeBtwAttack <= 0 )
        { if (playerMovement.isGrounded == true)
            {
                anim.SetTrigger("Attack");
                timeBtwAttack = startTimeBtwAttack;
            }
        else
            {
                anim.SetTrigger("JumpAttack");
                timeBtwAttack = startTimeBtwAttack*0.5f;
            }
        }     
    }
    public void RealeaseDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("attack");
        }
    }
    private void OnPowerfulAttack()
    {
        if (timeBtwAttack <= 0)
        {
            anim.SetTrigger("PowerfulAttack");
            timeBtwAttack = 2 * startTimeBtwAttack;
        }
    }
    public void ReleasePowerfulDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage * 2);
            Debug.Log("Powerfulattack");
        }
    }
    public void ReleaseJumpDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(jumpAttackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("JumpAttack");
        }
    }
}
