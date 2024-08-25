using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; 
    public Transform[] spawnPoints; 
    public float minSpawnDelay; 
    public float maxSpawnDelay; 
    public float itemLifetime; 

    void Start()
    {
        StartCoroutine(SpawnRandomItem());
    }

    IEnumerator SpawnRandomItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));

            int itemIndex = Random.Range(0, items.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

            GameObject item = Instantiate(items[itemIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            item.AddComponent<RotateObject>();
            Destroy(item, itemLifetime); 
        }
    }
}