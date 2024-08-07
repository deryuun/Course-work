using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemiesSpawner spawner;
    public Wave[] waves;
    private int currentWaveIndex = 0;

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
            waveAnnouncer.AnnounceWave(currentWaveIndex + 1);
            spawner.SpawnWave(waves[currentWaveIndex]);
        }
    }

    void OnWaveComplete()
    {
        Debug.Log($"Волна {currentWaveIndex + 1} завершена.");
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
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
}
