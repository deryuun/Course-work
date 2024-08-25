using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWaveIndex = 0;

    public EnemiesSpawner spawner;
    public Wave[] waves;
    public GameOverMenu gameOver;
    public WaveAnnouncer waveAnnouncer;
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth.OnHealthDepleted += GameOver;
        spawner.OnAllEnemiesSpawned += OnWaveComplete;
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            if (currentWave.isBossWave)
            {
                waveAnnouncer.AnnounceBoss(); 
            }
            else
            {
                waveAnnouncer.AnnounceWave(currentWaveIndex + 1);
            }
        }
        spawner.SpawnWave(waves[currentWaveIndex]);
    }
    
    void OnWaveComplete()
    {
        Debug.Log($"Волна {currentWaveIndex + 1} завершена.");
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            CheckTasksCompletion(currentWaveIndex);
            StartNextWave();
        }
        else
        {
            Debug.Log("Все волны пройдены!");
        }
    }

    void OnDestroy()
    {
        spawner.OnAllEnemiesSpawned -= OnWaveComplete;
        playerHealth.OnHealthDepleted -= GameOver;
    }

    public void GameOver()
    {
        gameOver.Setup(currentWaveIndex + 1);
        Time.timeScale = 0;
    }
    
    void CheckTasksCompletion(int wave)
    {
        switch (wave)
        {
            case 1:
                GlobalTasksManager.Instance.CompleteTask(0);
                break;
            case 3:
                GlobalTasksManager.Instance.CompleteTask(1); 
                break;
            case 5:
                GlobalTasksManager.Instance.CompleteTask(2); 
                break;
            case 7:
                GlobalTasksManager.Instance.CompleteTask(3); 
                break;
            case 9:
                GlobalTasksManager.Instance.CompleteTask(4); 
                break;
        }
    }
}
