using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int numberOfHearts;

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    Rigidbody2D hero;
    float posX, posY; // изначальные позиции
    public void TakeEnemyDamage(int attackdamage)
    {
        health -= attackdamage;
    }
    void Start()
    {
        hero = GetComponent<Rigidbody2D>();
        posX = hero.position.x;
        posY = hero.position.y;
    }
    void Update()
    {
        if (health <= 0)
        {
            hero.position = new Vector2(posX, posY);
            health = 4;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (health > numberOfHearts)
            {
                health = numberOfHearts;
            }

            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
