using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Animator anim;
    public Transform attackPos;
    private Transform target;
    Rigidbody2D rb;
    public LayerMask player;
    public float radius;
    public float health=2;
    public float speed;
    public float stoppingdistance;     //дистанция где надо останавливаться
    public float fardistance;       //дальность обзора, дистанция с которой враг видит игрока
    public float timer = 2f;
    public float canmovetime = 2f; // как долго будет стоять после атаки
    public int attackdamage;
    public LayerMask Player;
    public RaycastHit2D[] result = new RaycastHit2D[1];
    public float distance;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeBtwAttack -= Time.deltaTime;
        if (health <= 0)
        { Destroy(gameObject); }
        if (Vector2.Distance(transform.position, target.position) > stoppingdistance && Vector2.Distance(transform.position, target.position) < fardistance && canmovetime <= timer)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.Translate(Vector2.right * Time.deltaTime * speed);
            anim.SetTrigger("isWalking");
        }
        if (Vector2.Distance(transform.position, target.position) <= stoppingdistance)
        {
            
            Attack();
          //  transform.position = Vector2.MoveTowards(transform.position, target.position, 0 * Time.deltaTime);
            transform.Translate(Vector2.right * Time.deltaTime * speed * 0);
            timer = 0f;

            //attack anim,
        }
        if (timer < canmovetime)
        { timer += Time.deltaTime; }


        if (target.transform.position.x < transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            anim.SetTrigger("Attack");
            timeBtwAttack = startTimeBtwAttack;
            result = Physics2D.CircleCastAll(attackPos.position, radius, attackPos.forward, distance, Player);

            for (int i = 0; i < result.Length; i++)
            {
                Debug.Log(result[i].collider.name);
            }
        }    
    }

    public void TakeDamage(int damage, bool FaceRight)
    {
        health -= damage;
        rb.AddForce(transform.up * 1900 + transform.right * 800*-1);
       
    }
 
}
