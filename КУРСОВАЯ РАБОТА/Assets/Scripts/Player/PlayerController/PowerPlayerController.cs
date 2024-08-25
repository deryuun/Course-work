using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlayerController : MonoBehaviour
{
    // Variables
    [SerializeField] private float speed = 5;
    private float currentSpeed;
    private bool _faceRight = true;
    public bool isFrozen = false;
    
    // Component references
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color freezeColor;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    public Animator animator;
    public VectorValue pos;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        transform.position = pos.initialValue;
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = PlayerStats.Instance.CurrentSpeed;
    }

    
    void Update()
    {
        if (!isFrozen)
        {
            Move();
            ReflectPlayer();
        }
        else
        {
            ChangeAnimation("Animation Idle");
        }
    }

    void ChangeAnimation(string animation)
    {
        animator.Play(animation);
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
        if (!isFrozen)
        {
            _rb.MovePosition(_rb.position + currentSpeed * Time.fixedDeltaTime * _direction);
        }
    }
    
    public void ModifySpeed(float factor, float duration)
    {
        StartCoroutine(SpeedModifier(factor, duration));
    }

    private IEnumerator SpeedModifier(float factor, float duration)
    {
        currentSpeed *= factor;
        yield return new WaitForSeconds(duration);
        currentSpeed = speed;
    }
    
    public void FreezePlayer(float duration)
    {
        if (!isFrozen)
        {
            StartCoroutine(Freeze(duration));
        }
    }

    private IEnumerator Freeze(float duration)
    {
        isFrozen = true;
        ChangeColor(freezeColor); 
        yield return new WaitForSeconds(duration);
        isFrozen = false;
        ChangeColor(originalColor);
    }
    
    private void ChangeColor(Color color)
    {
        spriteRenderer.color = color;
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
