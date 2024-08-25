using UnityEngine;

public class Task : MonoBehaviour
{
    public string achievement;
    public string description;
    public bool isCompleted;
    public int reward;

    public Task(string achievement, string description, int reward)
    {
        this.achievement = achievement;
        this.description = description;
        this.reward = reward;
        isCompleted = false;
    }
}
