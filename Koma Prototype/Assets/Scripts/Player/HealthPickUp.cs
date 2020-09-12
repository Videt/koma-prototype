using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    Health health;
    public int bonusHealth = 1;
    void Awake()
    {
        health = FindObjectOfType<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health.health < health.maxHealth)
        {
            Destroy(gameObject);
            health.health += bonusHealth;
        }
    }
}
