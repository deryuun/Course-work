using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemy : Enemy
{
    public float attackDistance = 1.5f;
    public float attackCooldown = 5f; 
    public float damage;

    private bool _isAttacking = false;
    void FixedUpdate()
    {
        if (playerCharacter.currentHealth <= 0 || health <= 0)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!_isAttacking)
        {
            if (distanceToPlayer <= attackDistance)
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
        Vector3 targetPosition = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        ChangeAnimation("ZombieWalk"); 
        AdjustFacing(targetPosition);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        ChangeAnimation("ZombieAttack");

        DealDamage();

        yield return new WaitForSeconds(attackCooldown); 
        _isAttacking = false;
    }
    
    private void DealDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            playerCharacter.TakeDamage(damage);
            player.GetComponent<PowerPlayerController>().ModifySpeed(0.3f, 3f);
        }
    }

    private void AdjustFacing(Vector3 targetPosition)
    {
        transform.localScale = targetPosition.x > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
}
