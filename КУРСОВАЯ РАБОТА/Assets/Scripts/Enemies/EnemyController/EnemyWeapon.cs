using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject magicPrefab;
    
    public float speed = 20f;
    
    public void Shoot(Vector2 direction)
    {
        if (magicPrefab && firePoint)
        {
            GameObject projectile = Instantiate(magicPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.velocity = direction * speed;
                projectile.transform.right = direction;
            }
        }
    }
}