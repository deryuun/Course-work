using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject magicPrefab;
    public Camera mainCamera;
    public GameObject player;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1")  && !player.GetComponent<PowerPlayerController>().isFrozen)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 shootingDirection = GetShootingDirection();
        GameObject magic = Instantiate(magicPrefab, firePoint.position, Quaternion.identity);
        magic.transform.right = shootingDirection; 
    }

    Vector2 GetShootingDirection()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootingDirection = (mousePosition - (Vector2)firePoint.position).normalized;
        return shootingDirection;
    }
}