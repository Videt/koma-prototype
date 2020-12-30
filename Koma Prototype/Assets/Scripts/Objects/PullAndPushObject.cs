using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(FixedJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class PullAndPushObject : MonoBehaviour
{
	public bool isBeingPushed = false;
	//private float xPos;

	public float defaultMass = 3;
	public float immovableMass = 100;

	public FixedJoint2D fixedJoint2D;

	private Rigidbody2D boxRigidbody;

	void Start()
	{
		boxRigidbody = GetComponent<Rigidbody2D>();
		fixedJoint2D.enabled = false;
		//xPos = transform.position.x;
	}

	void FixedUpdate()
	{
		/*if (isBeingPushed == false)
		{
			transform.position = new Vector3(xPos, transform.position.y);
		}
		else
		{
			xPos = transform.position.x;
		}*/

		if (isBeingPushed == false)
		{
			//boxRigidbody.bodyType = RigidbodyType2D.Static;
			boxRigidbody.mass = immovableMass;
		}
		else
		{
			//boxRigidbody.bodyType = RigidbodyType2D.Dynamic;
			boxRigidbody.mass = defaultMass;
		}
	}
}
