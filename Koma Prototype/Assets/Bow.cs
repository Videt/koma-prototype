using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    private float center, count; //count это переменная для счёта
    public Transform bow;
    public Transform player;  //трансформ игрока
    public GameObject Arrow, mainPoint, arrow;
    GameObject MainPoint;
    public float speed;     // скорость стрелы
    public float speedMain; //скорость вспомогательной цели
    public Rigidbody2D rb;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            count = Vector2.Distance(bow.position, player.position);
            center = count / 4; // center - переменная для вспомогательной точки, которая двигается к игроку
           Vector2 mainpoint = new Vector2((bow.position.x + player.position.x)/2 , center);
            MainPoint = Instantiate(mainPoint, mainpoint, Quaternion.identity);
            Arrow = Instantiate(arrow, transform.position, Quaternion.identity);
            rb = Arrow.GetComponent<Rigidbody2D>();
        }
        // движение стрелы по дуге
        Arrow.transform.position = Vector2.MoveTowards(Arrow.transform.position, MainPoint.transform.position, speed * Time.deltaTime);

        Vector2 dir = MainPoint.transform.position - Arrow.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        //движение вспомогательной точки к игроку
        MainPoint.transform.position = Vector2.MoveTowards(MainPoint.transform.position, player.position, speedMain*Time.deltaTime);
    }
}
