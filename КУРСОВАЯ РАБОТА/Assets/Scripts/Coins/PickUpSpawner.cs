using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CoinPrefab;

    public void DropItems(int count, int coinsValue = 1)
    {
        while (count-- > 0)
        {
            GameObject coin = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
            Pickup pickup = coin.GetComponent<Pickup>();
            if (pickup != null)
            {
                pickup.SetCoins(coinsValue);
            }
        }
    }
}
