using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isFrozen;

    public Shield shieldTimer;
    public GameObject shield;
    private Color originalColor;
    public Color hurtColor = new Color(1f, 0.5f, 0.5f, 0.5f);
    public HealthBar healthBar;
    private SpriteRenderer spriteRenderer;
    public event Action OnHealthDepleted;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        healthBar.maxHealth = maxHealth;
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(float damage)
    {
        if (!shieldTimer.isCooldown)
        {
            currentHealth -= damage;
            healthBar.HP = currentHealth;
            if (currentHealth <= 0)
            {
                Die();
                OnHealthDepleted?.Invoke();
            }
            else if (!isFrozen)
            {
                StartCoroutine(FlashColor(hurtColor, 0.2f));
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
    private IEnumerator FlashColor(Color color, float duration)
    {
        spriteRenderer.color = color; 
        yield return new WaitForSeconds(duration); 
        spriteRenderer.color = originalColor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Potion"))
        {
            ChangeHealth(7);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Shield"))
        {
            if (!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCooldown = true;
                Destroy(other.gameObject);
            }
            else
            {
                shieldTimer.ResetTimer();
                Destroy(other.gameObject);
            }
        }
    }

    private void ChangeHealth(int health)
    {
        if(currentHealth != maxHealth)
        {
            currentHealth += health;
            if (currentHealth > 100)
            {
                currentHealth = 100;
                healthBar.HP = currentHealth;
            }
            else
            {
                healthBar.HP = currentHealth;
            }
        }
    }
}
