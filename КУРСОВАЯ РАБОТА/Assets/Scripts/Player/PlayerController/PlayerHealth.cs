using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float currentDamage = 10;
    public float defensePercentage;
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
        currentDamage = PlayerStats.Instance.CurrentDamage;
        defensePercentage = PlayerStats.Instance.CurrentDefense;
    }

    public void TakeDamage(float enemyDamage)
    {
        if (!shieldTimer.isCooldown)
        {
            float damageReduced = enemyDamage * (1 - defensePercentage / 100f);
            currentHealth -= damageReduced;
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
    
    public void ApplyBurnEffect(float initialDamage, float burnDamage, float duration)
    {
        TakeDamage(initialDamage);
        StartCoroutine(Burn(burnDamage, duration));
    }

    private IEnumerator Burn(float damage, float duration)
    {
        int count = 3;  
        while (count > 0)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(duration / 4);  
            count--;
        }
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
