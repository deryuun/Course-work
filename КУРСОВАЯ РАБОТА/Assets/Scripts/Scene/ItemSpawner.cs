using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; // Массив префабов предметов для спавна
    public Transform[] spawnPoints; // Точки, где могут появляться предметы
    public float minSpawnDelay = 5f; // Минимальная задержка между спавнами
    public float maxSpawnDelay = 15f; // Максимальная задержка между спавнами
    public float itemLifetime = 10f; // Время жизни предмета после спавна

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
            Destroy(item, itemLifetime); // Уничтожить объект через itemLifetime секунд
        }
    }
}