using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    public float speed;
    public float damage;
    public Rigidbody2D rb;
    public GameObject impactEffect;
    void Start()
    {
        damage = PlayerStats.Instance.CurrentDamage;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            BossEnemy enemy = hitInfo.GetComponent<BossEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
