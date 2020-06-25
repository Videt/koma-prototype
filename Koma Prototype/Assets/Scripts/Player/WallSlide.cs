﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{
    public float distance = 2f;
    PlayerMovement player;
    public float speed = 2f;
    public Transform RayPoint;
    void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(RayPoint.transform.position, transform.right*-1, distance);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (player.isGrounded == false && hit.collider != null)
        {
            if (GetComponent<Rigidbody2D>().velocity.y < speed)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            }
        }
    }
}