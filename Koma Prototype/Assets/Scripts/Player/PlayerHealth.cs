using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Image[] heart_container;
    public Sprite full_heart;
    public Sprite empty_heart;
    public Transform HeartContainer;

    private void OnEnable()
    {
        UpdateHealth();
    }

    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }    

      

    }
    public void HeartAdd(int addCount)
    {
        maxHealth += addCount;
        UpdateHealth();
    }

    public void AddHp(int AddHp)
    {
        health += AddHp;
        UpdateHealth();
    }

    public void IsDamaged(int Damage)
    {
        health -= Damage;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        for (int i = 0; i < heart_container.Length; i++)
        {
            if (i < health)
            {
                heart_container[i].sprite = full_heart;
            }
            else
            {
                heart_container[i].sprite = empty_heart;
            }

            if (i < maxHealth)
            {
                heart_container[i].enabled = true;
            }
            else
            {
                heart_container[i].enabled = false;
            }
        }
    }
}
  

