using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuOutside : MonoBehaviour
{
    public static bool pauseGame;
    public GameObject pauseMenu;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void LoadLobby()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false; 
        SceneManager.LoadScene("SampleScene");
    }
}

