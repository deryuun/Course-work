using System.Collections;
using UnityEngine;

public class OrcEnemy : Enemy
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
        ChangeAnimation("OrcWalk"); 
        AdjustFacing(targetPosition);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        ChangeAnimation("OrcAttack"); 

        yield return new WaitForSeconds(1); 

        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            playerCharacter.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown - 1); 
        _isAttacking = false;
    }

    private void AdjustFacing(Vector3 targetPosition)
    {
        transform.localScale = targetPosition.x > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
}