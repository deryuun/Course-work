using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;
    public float speed = 4f;
    public int health;
    public int coinsToDrop;
    protected float RandomX;
    protected float RandomY;

    public PickUpSpawner pickupSpawner;
    public GameObject player;
    public event Action OnDeath;
    private SpriteRenderer spriteRenderer;
    public Color hurtColor;
    private Color originalColor;
    protected PlayerHealth playerCharacter;
    public Animator animator;

    protected virtual void Awake()
    {
        RandomX = Random.Range(1.0f, 5.0f);
        RandomY = Random.Range(-2.0f, -4.0f);
        pickupSpawner = FindObjectOfType<PickUpSpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        playerCharacter = player.GetComponent<PlayerHealth>();
    }
    
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        health -= (int)damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashColor(hurtColor, 0.2f));
        }
    }

    protected void Die()
    {
        if (isDead) return;
        
        isDead = true;
        health = 0;
        OnDeath?.Invoke();
        ChangeAnimation("DeathEffect");
        Destroy(gameObject, 0.5f);
        pickupSpawner.DropItems(coinsToDrop);
    }

    protected virtual void Attack() { }
    
    private IEnumerator FlashColor(Color color, float duration)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(duration); 
        spriteRenderer.color = originalColor;
    }
    
    public void ChangeAnimation(string animation)
    {
        animator.PlayInFixedTime(animation);
    }
}
