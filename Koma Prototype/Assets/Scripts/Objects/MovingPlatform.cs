using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    private bool movingDown = false;

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movingDown = true;
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            movingDown = false;
            collision.collider.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {
        if (movingDown)
        {
            velocity = new Vector3(0, -1, 0);
            transform.position += (velocity * Time.deltaTime);
        }
        else
        {
            velocity = (initialPosition - transform.position) * 1.8f; //умножаю на число, чтобы был эффект подпрыгивания (всплытия)
            //velocity = new Vector3(VelocityToInitialPositionX(), VelocityToInitialPositionY(), 0);
            transform.position += (velocity * Time.deltaTime);
        }
    }

    /*private float VelocityToInitialPositionX()
    {
        return Mathf.Round(initialPosition.x - transform.position.x);
    }

    private float VelocityToInitialPositionY()
    {
        return Mathf.Round(initialPosition.y - transform.position.y);
    }*/
}
