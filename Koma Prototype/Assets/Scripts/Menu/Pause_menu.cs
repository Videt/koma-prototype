using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_menu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject options;


    void Update()
    {
  if (Input.GetKeyDown(KeyCode.Escape))  // проверка на нажатие esc
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
   public void Resume()  //продолжить
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
     
        
    }
   
    public void BackFormUpgrade()
    {
        pauseMenuUI.SetActive(true);
      
    }
    public void Options() //открыть опции
    {
        options.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void Back() //вернуться в меню из опций или еще чего
    {
        options.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    void Pause()// пауза
    {
        pauseMenuUI.SetActive(true);
       
        Time.timeScale = 0f;
        GameIsPaused = true;

    }

    public void LoadMenu()//выход в главное меню
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame() //выход из игры
    {
      
        Application.Quit();
    }


}
