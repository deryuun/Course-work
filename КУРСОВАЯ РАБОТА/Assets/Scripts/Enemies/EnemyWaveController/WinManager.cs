using System.Collections;
using UnityEngine;
using TMPro;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public TextMeshProUGUI subtitleText;
    
    public float scrollSpeed = 20f;
    public string message = "The boss is defeated! " +
                            "You coped with 9 waves of enemies and were able to defeat the boss! " +
                            "I would like to thank you very much for passing my game. " +
                            "Stay tuned, new levels are just around the corner!";

    private void Start()
    {
        winPanel.SetActive(false);
        subtitleText.text = message;
    }

    private IEnumerator MoveText()
    {
        float targetPositionY = Screen.height;

        while (subtitleText.rectTransform.anchoredPosition.y < targetPositionY)
        {
            float newY = subtitleText.rectTransform.anchoredPosition.y + scrollSpeed * Time.deltaTime;
            subtitleText.rectTransform.anchoredPosition = new Vector2(subtitleText.rectTransform.anchoredPosition.x, newY);
            yield return null;
        }

        subtitleText.gameObject.SetActive(false);
    }
    
    public void StartScrolling()
    {
        winPanel.SetActive(true);
        StartCoroutine(MoveText());
    }
}