using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int numberOfHearts;


    GameMaster gm;
    public void TakeEnemyDamage(int attackdamage)
    {
        health -= attackdamage;
    }
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPos;
    }
    void Update()
    {
        if (health <= 0)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);
            health = 4;
        }      
    }
}
