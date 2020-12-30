using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerPushAndPull : MonoBehaviour
{
	public float distance = 0.5f;

	private GameObject box;

	public bool isHoldingObject = false;

	private Vector2 rayDirection;

	private Vector2 axis;

	private Rigidbody2D playerRigidBody;

	private bool isPushingButtonPressed = false;

    void Start()
    {
		playerRigidBody = GetComponent<Rigidbody2D>();
	}

    void Update()
	{
		axis.x = Input.GetAxis("Horizontal");

		FlipRay();

		if (Input.GetKeyDown(KeyCode.X))
        {
			isPushingButtonPressed = true;
        }
		else if(Input.GetKeyUp(KeyCode.X))
        {
			isPushingButtonPressed = false;
        }
	}

    void FixedUpdate()
    {
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection * transform.localScale.x, distance);

		//если нажимаем кнопку, то предмет присоединяется к игроку
		if (hit.collider != null && isPushingButtonPressed == true && hit.collider.gameObject.CompareTag("Pushable"))
		{
			isHoldingObject = true;
			box = hit.collider.gameObject;

			//присоединяем движимый объект к игроку
			box.GetComponent<FixedJoint2D>().connectedBody = playerRigidBody;
			box.GetComponent<FixedJoint2D>().enabled = true;

			box.GetComponent<PullAndPushObject>().isBeingPushed = true;
		}
		//если отжимает кнопку, то отсоединяем
		else if (box != null && isPushingButtonPressed == false)
		{
			isHoldingObject = false;
			box.GetComponent<FixedJoint2D>().enabled = false;
			box.GetComponent<PullAndPushObject>().isBeingPushed = false;
		}
	}

    //изменить направление рейкаста в зависимости от направления движения перса
    void FlipRay()
    {
		if (axis.x < 0)
		{
			rayDirection = Vector2.left;
		}
		else if (axis.x > 0)
		{
			rayDirection = Vector2.right;
		}
	}
}
