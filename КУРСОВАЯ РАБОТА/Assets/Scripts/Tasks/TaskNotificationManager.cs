using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskNotificationManager : MonoBehaviour
{
    public static TaskNotificationManager Instance { get; private set; }

    public GameObject notificationPanel;
    public TextMeshProUGUI taskNameText;
    public Image checkmarkImage;
    public float displayTime = 2.0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        notificationPanel.SetActive(false);
    }

    public void ShowNotification(string taskName)
    {
        if (!notificationPanel.activeSelf) 
        {
            notificationPanel.SetActive(true); 
        }
        Debug.Log("Showing notification for task: " + taskName);
        taskNameText.text = taskName;
        StartCoroutine(ShowAndHideNotification());
    }

    private IEnumerator ShowAndHideNotification()
    {
        checkmarkImage.enabled = true;

        CanvasGroup canvasGroup = notificationPanel.GetComponent<CanvasGroup>();
        float time = 0;
        while (time < 1.0f)
        {
            time += Time.deltaTime / 0.5f;
            canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        time = 0;
        while (time < 1.0f)
        {
            time += Time.deltaTime / 0.5f;
            canvasGroup.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
        }

        notificationPanel.SetActive(false);
    }
}