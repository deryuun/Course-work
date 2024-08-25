using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullEnemy : Enemy
{
    public float attackStartDistance = 10f;
    public float attackEndDistance = 8f;
    public float attackCooldown = 5f;
    private bool _isAttacking = false;
    private bool _attackRoutineActive = false;

    public EnemyWeapon weapon;
    public int selfDamage = 10;
    
    protected override void Awake()
    {
        base.Awake();
    }

    void FixedUpdate()
    {
        if (playerCharacter.currentHealth <= 0 || health <= 0)
        {
            return; 
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (_isAttacking && distanceToPlayer > attackEndDistance)
        {
            _isAttacking = false;
        }

        if (!_isAttacking && !_attackRoutineActive)
        {
            if (distanceToPlayer <= attackStartDistance)
            {
                StartCoroutine(AttackRoutine());
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }
    
    private void MoveTowardsPlayer()
    {
        Vector3 moveDirection = (player.transform.position - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;

        ChangeAnimation("FireSkullWalk");

        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    private IEnumerator AttackRoutine()
    {
        _attackRoutineActive = true;
        _isAttacking = true;
        yield return new WaitForSeconds(1);
        Attack();
        yield return new WaitForSeconds(attackCooldown); 
        _isAttacking = false;
        _attackRoutineActive = false;
    }

    protected override void Attack()
    {
        Vector2 shootDirection = (player.transform.position - transform.position).normalized;
        weapon.Shoot(shootDirection);
        TakeDamage(selfDamage);
    }
}
