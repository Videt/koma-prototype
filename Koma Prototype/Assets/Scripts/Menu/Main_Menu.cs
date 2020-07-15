using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public AudioClip MainMusic;
    public GameObject options;
    public GameObject MenuUI;
    public void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(MainMusic);

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void Options()
    {
        options.SetActive(true);
        MenuUI.SetActive(false);
    }
    public void Back() //вернуться в меню из опций или еще чего
    {
        options.SetActive(false);
        MenuUI.SetActive(true);
    }
}
