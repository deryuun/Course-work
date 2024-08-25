using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool isGameOver = false;
    public float transitionDuration = 1.0f; 

    public TextMeshProUGUI scoreText;
    public PlayerHealth player;
    private CanvasGroup canvasGroup;
    
    void Awake()
    {
        if (!player)
        {
            Debug.LogError("PlayerHealth component is not assigned.");
            return;
        }
        player.OnHealthDepleted += HandleGameOver;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        float counter = 0;
        while (counter < transitionDuration)
        {
            counter += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, counter / transitionDuration);
            yield return null;
        }
    }
    private void OnDestroy()
    {
        if (player)
        {
            player.OnHealthDepleted -= HandleGameOver;
        }
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0; 
        isGameOver = true;
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        if (score - 1 <= 0)
        {
            scoreText.fontSize = 10;
            scoreText.text = "You haven't passed a single wave..."; 
        }
        else
        {
            scoreText.text = "WAVE " + (score - 1); 
        }
        StartCoroutine(FadeIn());
    }

    public void LoadLobby()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
