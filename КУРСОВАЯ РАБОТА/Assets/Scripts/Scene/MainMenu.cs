using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Debug.Log("Game over");
        Application.Quit();
    }

    public void OnButtonPressed()
    {
        optionsPanel.SetActive(true);
    }

    public void OnCloseButtonPressed()
    {
        optionsPanel.SetActive(false);
    }
}
