using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool isFire;
    public float initialDamage;
    public float burnDamage;
    public float duration;
    
    public Rigidbody2D rb;
    public GameObject impactEffect;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealth player = hitInfo.GetComponent<PlayerHealth>();
            if (player != null)
            {
                if (isFire)
                {
                    player.GetComponent<PlayerHealth>().ApplyBurnEffect(initialDamage, burnDamage, duration);
                }
                else
                {
                    player.TakeDamage(damage);
                }
            }

            Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}