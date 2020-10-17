using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform attackCheckPos;
    public LayerMask enemyLayerMask;
    public float attackDistance;
    public float damage;
    public float damage_delay;

    public bool attack;

    void Start()
    {
        
    }

  
    void Update()
    {
        Draw();
        if (Input.GetMouseButtonDown(0) && !attack) //если нажали на кнопку и не атакуем
        {          
            StartCoroutine(Attack()); 
            attack = true; // атакуем true
        }
    }
   
    IEnumerator Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackCheckPos.position, transform.position, attackDistance, enemyLayerMask); // рейкаст проверяет смотрим ли мы на противника
        if (hit.collider != null)
        {
            Enemy_Health enemy = hit.collider.GetComponent<Enemy_Health>(); // записываем противника чтобы менять его параментры
            enemy.Damaged(damage); // у противника вызываем метод damage
        }
            yield return new WaitForSeconds(damage_delay); // ждем
            attack = false; // атакуем false
    }

    void Draw()
    {
        Debug.DrawRay(attackCheckPos.position, transform.right, Color.red, 0); // рисуем линию по направлению рейкаста по приколу 
    }
   
}
