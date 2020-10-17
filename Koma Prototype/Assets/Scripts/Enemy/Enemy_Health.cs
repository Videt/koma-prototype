using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float health;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void Damaged(float gived_damage)
    {
        health -= gived_damage;
    }
}
