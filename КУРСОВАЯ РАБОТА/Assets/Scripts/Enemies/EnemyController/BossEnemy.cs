using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BossEnemy : MonoBehaviour
{
    public event Action OnDeath;
    public Animator animator;
    public WinManager subtitleScroll;
    public GameObject player;
    public PlayerHealth playerCharacter;
    public PickUpSpawner pickupSpawner;
    public BossHealthBar healthBar;
    public CapsuleCollider2D bossCollider;
    
    public float fleeDistance;
    public float attackRange;
    public float fireBreathRange;
    public float earthquakeRange;
    public float attackCooldown;
    public float slimeHealth = 200;
    public float demonHealth;
    public float speed;

    private bool isInAttackAnimation;
    private bool isTakingHit;
    private bool isSlime = true;
    private bool isSpawning = false;
    private float cooldownTimer = 0;

    void Start()
    {
        subtitleScroll = FindObjectOfType<WinManager>();
        healthBar = FindObjectOfType<BossHealthBar>(); 
        healthBar.ActivateHealthBar();
        healthBar.maxSlimeHealth = slimeHealth; 
        healthBar.slimeHp = slimeHealth;
        pickupSpawner = GetComponent<PickUpSpawner>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCharacter = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        bossCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (isSpawning || (slimeHealth <= 0 && demonHealth <= 0) || isInAttackAnimation || animator.GetCurrentAnimatorStateInfo(0).IsTag("attack")
            || playerCharacter == null || playerCharacter.currentHealth <= 0) return;
        
        if (isSlime)
        {
            HandleSlimeBehavior();
        }
        else
        {
            HandleDemonBehavior();
        }
    }

    private void HandleSlimeBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < fleeDistance)
        {
            FleeFromPlayer();
        }
        else
        {

            ChangeAnimation("idle");
        }
    }

    private void HandleDemonBehavior()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        cooldownTimer -= Time.deltaTime;

        if (!isSpawning && demonHealth > 0 && !isInAttackAnimation)
        {
            if (distance > earthquakeRange && !isInAttackAnimation)
            {
                ChangeAnimation("d_walk");
                FollowPlayer();
            }
            else
            {
                ChangeAnimation("d_idle");
            }
        }
        if (cooldownTimer <= 0 && !isSpawning)
        {
            cooldownTimer = attackCooldown;
            if (distance <= attackRange)
            {
                MeleeAttack();
            }
            else if (distance <= fireBreathRange)
            {
                FireBreath();
            }
            else if (distance <= earthquakeRange)
            {
                EarthquakeAttack();
            }
        }
    }
    
    public void DealDamage(float damage)
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= GetCurrentAttackRange() && GetCurrentAttackRange() == earthquakeRange)
        {
            playerCharacter.isFrozen = true;
            player.GetComponent<PowerPlayerController>().FreezePlayer(2);
            playerCharacter.TakeDamage(damage);
            playerCharacter.isFrozen = false;
            if (distance <= attackRange)
            {
                MeleeAttack();
            }
        } else if (distance <= GetCurrentAttackRange() && GetCurrentAttackRange() == fireBreathRange)
        {
            player.GetComponent<PlayerHealth>().ApplyBurnEffect(damage, 2, 5);
        }
        else
        {
            playerCharacter.TakeDamage(damage);
        }
    }
    
    private float GetCurrentAttackRange()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("d_cleave")) return attackRange;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("d_fire_breath")) return fireBreathRange;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("d_smash")) return earthquakeRange;
        return 0;
    }
    
    private void FollowPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 fleeDirection = (transform.position - player.transform.position).normalized;
        float moveSpeed = speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(fleeDirection.x * moveSpeed, fleeDirection.y * moveSpeed, 0);
        newPosition.z = transform.position.z; 
        
        ChangeAnimation("move");
        
        transform.position = newPosition;
        
        if (fleeDirection.x > 0) 
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        } 
        else if (fleeDirection.x < 0) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
    
    private void EarthquakeAttack()
    {
        isInAttackAnimation = true;
        ChangeAnimation("d_smash");
        Invoke(nameof(ResetAttackState), 1.81f); 
    }

    private void MeleeAttack()
    {
        isInAttackAnimation = true;
        ChangeAnimation("d_cleave");
        Invoke(nameof(ResetAttackState), 1.51f);
    }

    private void FireBreath()
    {
        isInAttackAnimation = true;
        ChangeAnimation("d_fire_breath");
        Invoke(nameof(ResetAttackState), 2.11f);
    }

    public void TakeDamage(float damage)
    {
        if (isSlime)
        {
            slimeHealth -= damage;
            healthBar.slimeHp = slimeHealth;
            if (slimeHealth <= 0)
            {
                isSpawning = true;
                Die();
                TransitionToDemon();
            }
            else
            {
                isInAttackAnimation = true;
                ChangeAnimation("take_hit");
                Invoke(nameof(ResetAttackState), 0.58f);
            }
        }
        else
        {
            demonHealth -= damage;
            healthBar.demonHp = demonHealth;
                
            if (demonHealth <= 0)
            {
                Die();
            }
            else
            {
                isInAttackAnimation = true;
                ChangeAnimation("d_take_hit");
                Invoke(nameof(ResetAttackState), 0.5f);
            }
        }
    }
    
    private void Die()
    {
        if(isSlime)
        {
            isSlime = false;
            isSpawning = true;
            ChangeColliderForDemon();
            ChangeAnimation("death/transform");
            healthBar.ActivateDemonHealthBar();
            demonHealth = 500; 
            healthBar.demonHp = demonHealth;
            Invoke(nameof(ResetSpawning), 3.0f);
        }
        else
        {
            OnDeath?.Invoke();
            healthBar.DeactivateHealthBar();
            ChangeAnimation("death");
            Invoke(nameof(DestroyBoss), 2.2f);
            pickupSpawner.DropItems(20);
            OnBossDefeated();
        }
    }
    
    private void TransitionToDemon()
    {
        isSlime = false;
        healthBar.ActivateDemonHealthBar();  
        demonHealth = 500;
        healthBar.demonHp = demonHealth;     
    }
    
    private void ResetSpawning()
    {
        isSpawning = false; 
    }

    private void ResetAttackState()
    {
        isInAttackAnimation = false; 
    }
    private void DestroyBoss()
    {
        Destroy(gameObject);
    }
    
    private void ChangeColliderForDemon()
    {
        bossCollider.size = new Vector2(4.0f, 4.5f);
        bossCollider.offset = new Vector2(0, 2.5f);
    }
    
    void ChangeAnimation(string animation)
    {
        animator.Play(animation);
    }
    
    private void AdjustFacing(float directionX)
    {
        transform.localScale = directionX > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
    
    private void OnBossDefeated()
    {
        if (subtitleScroll != null)
        {
            subtitleScroll.StartScrolling(); 
        }
    }
}