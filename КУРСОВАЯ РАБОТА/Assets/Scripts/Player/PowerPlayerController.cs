using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 6;
    private bool _faceRight = true;
    private string _currentAnimation;
    
    // Component references
    private Vector2 _direction;
    private Rigidbody2D _rb;
    public Animator animator;
    public VectorValue pos;

    void Start()
    {
        transform.position = pos.initialValue;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        ReflectPlayer();
    }

    void ChangeAnimation(string animation)
    {
        if (_currentAnimation == animation) return;
        
        animator.Play(animation);
        _currentAnimation = animation;
    }

    void Move()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        if (_direction.x != 0 || _direction.y != 0)
        {
            ChangeAnimation("Animation Run");
        }
        else
        {
            ChangeAnimation("Animation Idle");
        }
    }
    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + speed * Time.fixedDeltaTime * _direction);
    }

    void ReflectPlayer()
    {
        if ((_direction.x > 0 && !_faceRight) ||
            (_direction.x < 0 && _faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = !_faceRight;
        }
    }
}
