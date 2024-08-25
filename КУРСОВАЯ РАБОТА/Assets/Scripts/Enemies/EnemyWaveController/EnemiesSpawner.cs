using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    private int enemiesToSpawn = 0;
    private int enemiesDead = 0;
    
    public Transform[] spawnPoints;
    public event Action OnAllEnemiesSpawned;

    public void SpawnWave(Wave wave)
    {
        enemiesDead = 0; 
        enemiesToSpawn = CalculateTotalEnemies(wave);
        StartCoroutine(SpawnEnemies(wave));
    }
    
    private int CalculateTotalEnemies(Wave wave)
    {
        int totalEnemies = 0;
        foreach (var enemy in wave.enemies)
        {
            totalEnemies += enemy.count;
        }
        return totalEnemies;
    }

    private IEnumerator SpawnEnemies(Wave wave)
    {
        foreach (var enemy in wave.enemies)
        {
            for (int i = 0; i < enemy.count; i++)
            {
                Vector3 spawnPosition = wave.isBossWave && i == 0 ? 
                    spawnPoints[wave.bossSpawnPointIndex].position : 
                    spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].position;
                
                InstantiateEnemy(enemy.enemyPrefab, spawnPosition);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }
    }

    private void InstantiateEnemy(GameObject prefab, Vector3 position)
    {
        var enemyInstance = Instantiate(prefab, position, Quaternion.identity);
        
        if (enemyInstance.GetComponent<Enemy>() == null)
        {
            var enemyComponent = enemyInstance.GetComponent<BossEnemy>();
            enemyComponent.OnDeath += HandleEnemyDeath;
        }
        else
        {
            var enemyComponent = enemyInstance.GetComponent<Enemy>();
            enemyComponent.OnDeath += HandleEnemyDeath;
        }
    }
    
    private void HandleEnemyDeath()
    {
        enemiesDead++;
        CheckWaveCompletion();
    }
    
    private void CheckWaveCompletion()
    {
        Debug.Log("Dead: " + enemiesDead);
        if (enemiesDead >= enemiesToSpawn)
        {
            OnAllEnemiesSpawned?.Invoke();
            Debug.Log("Все враги волны уничтожены, начинаем следующую волну.");
        }
    }
}