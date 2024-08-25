using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance; 
    public int DamageLevel { get; private set; } = 1;
    public int DefenseLevel { get; private set; } = 1; 
    public int SpeedLevel { get; private set; } = 1;

    public float CurrentSpeed { get; private set; } = 5.0f;
    public float CurrentDamage { get; private set; } = 15;
    public float CurrentDefense { get; private set; } = 1;
    
    public static PlayerStats Instance {
        get {
            if (_instance == null) 
            {
                var existingObject = FindObjectOfType<PlayerStats>();
                
                if (existingObject != null) 
                {
                    _instance = existingObject;
                } 
                else 
                {
                    var singletonObject = new GameObject("PlayerStats");
                    _instance = singletonObject.AddComponent<PlayerStats>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        UpdateStats();
    }

    public void IncreaseDamage(int amount)
    {
        DamageLevel++;
        CurrentDamage += amount;
    }

    public void IncreaseDefense(int percentage)
    {
        DefenseLevel++;
        CurrentDefense += percentage;
    }

    public void IncreaseSpeed(float amount)
    {
        SpeedLevel++;
        CurrentSpeed += amount;
    }

    void UpdateStats()
    {
        CurrentDamage = 10 + 5 * DamageLevel;
        CurrentDefense = DefenseLevel;  
        CurrentSpeed = 5.0f + 0.5f * SpeedLevel;  
    }
}