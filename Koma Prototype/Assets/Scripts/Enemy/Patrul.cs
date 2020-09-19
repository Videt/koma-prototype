using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrul : MonoBehaviour
{
    public bool isPatruling = true;
    public GameObject enemy;
    public GameObject player; // тут скрипт получит доступ к игроку
    public float speed;  //  скорость хотьбы
    private float waitTime;
    public float startWaitTime;
    public float distance; 
    public Transform[] moveSpots;   //массив с точками по которым надо ходить
    private int randomSpot;  

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        enemy.GetComponent<Enemy>().enabled = false;
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatruling)
        {
            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) >= 2f)// движение к точке
            {
                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            if (moveSpots[randomSpot].transform.position.x < transform.position.x) //поворот в сторону точки
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) <= 2f) //оставновка, если на допустимом расстоянии
            {
                if (waitTime <= 0)
                {
                    randomSpot = Random.Range(0, moveSpots.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        Debug.DrawRay(transform.position, transform.right * distance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                enemy.GetComponent<Enemy>().enabled = true;
                enemy.GetComponent<Patrul>().enabled = false;
            }
            if (hit.collider != null)
            {
                Debug.Log("hit: " + hit.collider.name);
                player = hit.collider.gameObject;
            }

        }

    }
}
