using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SpringJoint2D))]
public class GrapplingHook : MonoBehaviour
{
    public GameObject bullet;//нужно перенести сюда префаб, который будет использоваться как "пуля"

    public float bulletSpeed;

    public Transform shootPoint; //shoot point висит на крюке (hook)

    Vector2 direction; //направление крюка

    public LineRenderer lineRenderer;

    GameObject target;

    public SpringJoint2D springJoint; //висит на крюке

    void Start()
    {
        lineRenderer.enabled = false;
        springJoint.enabled = false;
    }
    
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePos - (Vector2)transform.position;

        FaceMouse();

        //если ничего не зацеплено за крюк, то просто стреляет крюком 
        if(Input.GetMouseButtonDown(0) && lineRenderer.enabled == false)
        {
            Shoot();
        }
        //но если зацеплено, то сначала отпускает вещь и только после этого стреляет
        else if (Input.GetMouseButtonDown(0) && lineRenderer.enabled == true)
        {
            Release();
            Shoot();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Release();
        }

        //рисует линию до объекта
        if (target != null)
        {
            lineRenderer.SetPosition(0, shootPoint.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
    }

    void FaceMouse()
    {
        transform.right = direction;
    }

    void Shoot()
    {
        GameObject bulletInstance = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        bulletInstance.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);

        //рисует линию от крюка до "пули"
        target = bulletInstance;
        lineRenderer.enabled = true;
    }

    void Release()
    {
        lineRenderer.enabled = false;
        springJoint.enabled = false;
        target = null;
    }

    public void TargetHit(GameObject hit)
    {
        target = hit;
        lineRenderer.enabled = true;
        springJoint.enabled = true;
        springJoint.connectedBody = target.GetComponent<Rigidbody2D>();
    }
}
