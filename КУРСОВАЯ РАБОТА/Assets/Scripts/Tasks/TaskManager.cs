using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> tasks;
    [SerializeField] private List<GameObject> taskObjects;
    [SerializeField] public DialogueManager dialogueManager;

    [HideInInspector] public bool isTaskPanelOpen = false;
    private void Start()
    {
        GlobalTasksManager.Instance.OnTaskChanged += UpdateAllTasksUI;
        UpdateAllTasksUI();
    }

    private void UpdateTaskUI(Task task, GameObject taskObject)
    {
        if (taskObject != null)
        {
            TMP_Text statusText = taskObject.transform.Find("Status/StatusText").GetComponent<TMP_Text>();
            TMP_Text rewardText = taskObject.transform.Find("Reward/RewardAmount").GetComponent<TMP_Text>();
            
            if (task.isCompleted)
            {
                statusText.text = "Completed";
                statusText.color = Color.gray;
            }
            else
            {
                statusText.text = "Not completed";
                statusText.color = new Color(229/255f, 37/255f, 84/255f, 1); 
            }
            
            rewardText.text = task.reward.ToString();
        }
    }

    public void OnCloseButtonPressed()
    {
        dialogueManager.CloseTasksPanel();
    }

    private void OnDestroy() {
        GlobalTasksManager.Instance.OnTaskChanged -= UpdateAllTasksUI;
    }

    private void UpdateAllTasksUI() {
        for (int i = 0; i < GlobalTasksManager.Instance.Tasks.Count && i < taskObjects.Count; i++) {
            UpdateTaskUI(GlobalTasksManager.Instance.Tasks[i], taskObjects[i]);
        }
    }

}
