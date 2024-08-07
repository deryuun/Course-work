using System.Collections;
using UnityEngine;

public class OrcEnemy : Enemy
{
    public float attackDistance = 1.5f; // Расстояние для начала атаки
    public float attackCooldown = 5f; // Пауза между атаками
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
        ChangeAnimation("OrcWalk"); // Анимация ходьбы
        AdjustFacing(targetPosition);
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        ChangeAnimation("OrcAttack"); // Анимация атаки

        // Ожидание момента в анимации, когда наносится урон (например, в момент удара)
        yield return new WaitForSeconds(1); // Предполагается, что удар происходит через 1 секунду после начала анимации

        // Повторная проверка расстояния перед нанесением урона
        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            playerCharacter.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown - 1); // Остаток времени кулдауна
        _isAttacking = false;
    }

    private void AdjustFacing(Vector3 targetPosition)
    {
        transform.localScale = targetPosition.x > transform.position.x ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
}