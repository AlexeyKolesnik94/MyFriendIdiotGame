using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private float _moveInput;
    private bool _facingRight = true;
    private bool _isGrounded;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D _rb;
    private Animator _animator;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            _rb.velocity = Vector2.up * jumpForce;
            _animator.SetTrigger("takeoff");
        }

        if (_isGrounded) {
            _animator.SetBool("isJumping", false);
        } else {
            _animator.SetBool("isJumping", true);
        }
    }

    private void FixedUpdate() {
        
        Run();

        if (!_facingRight && _moveInput > 0) {
            Flip();
        } else if (_facingRight && _moveInput < 0) {
            Flip();
        }

        if (_moveInput == 0) {
            _animator.SetBool("isRunning", false);
        } else {
            _animator.SetBool("isRunning", true);
        }
    }



    void Run() {
        _moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
    }

    void Flip() {
        _facingRight = !_facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
