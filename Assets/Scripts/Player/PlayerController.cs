using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _playerFeetPosition;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _inputValue;
    private bool _isGrounded;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Run();
    }

    private void Update() {
        Jump();
    }

    private void Run() {
        _inputValue = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_inputValue * _speed, _rb.velocity.y);

        if (_inputValue > 0 && _rb.transform.eulerAngles.y == 180) {
            _rb.transform.eulerAngles = Vector3.zero;
        } else if (_inputValue < 0 && _rb.transform.eulerAngles.y == 0) {
            _rb.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        _animator.SetBool("IsRunning", _inputValue != 0);
    }

    private void Jump() {
        _isGrounded = Physics2D.OverlapCircle(_playerFeetPosition.transform.position, 0.3f, _groundLayer);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            _rb.velocity = Vector2.up * _jumpForce;
            _animator.SetTrigger("TakeOff");
        }

        _animator.SetBool("IsJumping", !_isGrounded);
    }

    public void SetSpeed(float speed) => _speed = speed;

    public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;
}