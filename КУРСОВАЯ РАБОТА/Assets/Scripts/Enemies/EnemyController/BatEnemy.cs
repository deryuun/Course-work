using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BatEnemy : Enemy
    {
        private bool _isChasing = true;
        public int damage = 15;
        void FixedUpdate()
        {
            if (playerCharacter == null)
            {
                return;
            }
            if (playerCharacter.currentHealth <= 0 || health <= 0)
            {
                return;
            }
            
            if (_isChasing && health > 0)
            {
                Vector3 targetPosition = player.transform.position + new Vector3(RandomX, RandomY, 0);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
                                         speed * Time.deltaTime) + new Vector2(0.0001f, 0);
                
                if (targetPosition.x < transform.position.x)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
                else if (targetPosition.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y,
                        transform.localScale.z);
                }
            }
        }

        protected override void Attack()
        {
            if (playerCharacter != null && health > 0)
            {
                Die();
                playerCharacter.TakeDamage(damage);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Trigger"))
            {
                _isChasing = false;
                if (health <= 0)
                {
                    Die();
                }
                else
                {
                    Attack();
                }
            }
        }
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Trigger"))
            {
                _isChasing = true;
                if(health > 0)
                {
                    ChangeAnimation("Idle/ Fly");
                }
            }
        }
    }
