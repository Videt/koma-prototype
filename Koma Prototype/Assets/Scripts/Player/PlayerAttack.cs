using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public GameObject FrontPos;
    public GameObject BackPos;
    public Transform attackPos;
    public Transform jumpAttackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage = 1;
    public PlayerMove playerMovement;
    public PlayerJump playerJump;
    public Animator anim;
   


    PlayerControls controls;
    void Awake()
    {        
        Collider col = GetComponent<Collider>();
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
    
    public void RealeaseDamage()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage, playerMovement.faceright);
            Enemy enemyScript = enemies[i].GetComponent<Enemy>();

        }
        
    }
    private void OnAttack()
    {
        if (playerMovement.isRunning == false)
        {

            if (playerMovement.isWalking == true)
            {
                anim.SetTrigger("RunAttack");
            }
            else
            {
                    anim.SetTrigger("Attack");
                    timeBtwAttack = startTimeBtwAttack;
            }
        }
    }
    private void OnPowerfulAttack()
    {   if (playerMovement.isRunning == false)
        {
            if (playerMovement.isWalking == true)
            {
                anim.SetTrigger("RunAttack");
            }
            else
            {
                if (timeBtwAttack <= 0)
                {
                    anim.SetTrigger("PowerfulAttack");
                    timeBtwAttack = 2 * startTimeBtwAttack;
                }
            }
        }
    }
    public void ReleasePowerfulDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage*2, playerMovement.faceright);
            Enemy enemyScript = enemies[i].GetComponent<Enemy>();

        }
    }
    public void ReleaseJumpDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(jumpAttackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().TakeDamage(damage, playerMovement.faceright);
            Enemy enemyScript = enemies[i].GetComponent<Enemy>();

        }
    }
}
