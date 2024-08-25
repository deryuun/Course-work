using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdinaryPlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 6;
    private bool _faceRight = true;
    
    // Component references
    private PlayerInputManager _inputManager;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    public Animator animator;
    public VectorValue pos;

    void Start()
    {
        transform.position = pos.initialValue;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _inputManager = FindObjectOfType<PlayerInputManager>();
    }

    
    void Update()
    {
        if (!_inputManager.CanPlayerMove())
        {
            ChangeAnimation("AnimationIdle");
            _direction = Vector2.zero; 
             return; 
        }
        Move();
        ReflectPlayer();
    }

    public void ChangeAnimation(string animation)
    {
        animator.Play(animation);
    }

    void Move()
    {
        _direction.x = Input.GetAxisRaw("Horizontal");
        _direction.y = Input.GetAxisRaw("Vertical");

        if (_direction.x != 0 || _direction.y != 0)
        {
            ChangeAnimation("AnimationWalk");
        }
        else
        {
            ChangeAnimation("AnimationIdle");
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
            _faceRight = !_faceRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
}
