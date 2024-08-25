using System.Collections.Generic;
using UnityEngine;

public class GlobalTasksManager : MonoBehaviour
{
    private TaskNotificationManager notificationManager;
    private static GlobalTasksManager _instance;
    
    public delegate void OnTaskChangedHandler();
    public event OnTaskChangedHandler OnTaskChanged;
    
    public static GlobalTasksManager Instance {
        get {
            if (_instance == null) 
            {
                var existingObject = FindObjectOfType<GlobalTasksManager>();
                
                if (existingObject != null) 
                {
                    _instance = existingObject;
                } 
                else 
                {
                    var singletonObject = new GameObject("GlobalTasksManager");
                    _instance = singletonObject.AddComponent<GlobalTasksManager>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    public List<Task> Tasks { get; private set; } = new List<Task>();

    private void Awake() {
        if (_instance != null && _instance != this) 
        {
            Destroy(gameObject);
        } 
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        if (Tasks.Count == 0) 
        { 
            InitializeTasks();
        }
    }
    
    private void InitializeTasks()
    {
        Tasks.Add(new Task("Winner \tI", "Go through the first wave of monsters", 10));
        Tasks.Add(new Task("Winner \tII","Go through the third wave of monsters", 20));
        Tasks.Add(new Task("Winner \t\tIII","Go through the fifth wave of monsters", 30));
        Tasks.Add(new Task("Winner \t\tIV","Go through the seventh wave of monsters", 40));
        Tasks.Add(new Task("Winner \t\tV","Go through the ninth wave of monsters", 50));
        Tasks.Add(new Task("Immortal Witch", "Defeat the boss", 100));
        Tasks.Add(new Task("Absolute perfection", "Upgrade all skills to the last level", 200));
    }
    
    public void CompleteTask(int index) {
        if (index >= 0 && index < Tasks.Count && !Tasks[index].isCompleted)
        {
            Tasks[index].isCompleted = true;
            OnTaskChanged?.Invoke(); 
            CoinsManager.Instance.AddCoins(Tasks[index].reward / 10);
            Debug.Log("Task's name: " + Tasks[index].achievement);
            TaskNotificationManager.Instance.ShowNotification(Tasks[index].achievement);
        }
    }
}