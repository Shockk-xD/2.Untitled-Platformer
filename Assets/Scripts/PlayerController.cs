using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _feetPosition;

    private Rigidbody2D _rb;
    private float _inputValue;
    private bool _isGrounded;
    private Animator _animator;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        _inputValue = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(_inputValue * _speed, _rb.velocity.y);
    }

    private void Update() {
        _isGrounded = Physics2D.OverlapCircle(_feetPosition.transform.position, 0.3f, _groundLayer);

        if (_isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            _rb.velocity = Vector2.up * _jumpForce;
            _isGrounded = false;
            _animator.SetTrigger("takeOff");
        }

        if (_isGrounded)
            _animator.SetBool("isJumping", false);
        else
            _animator.SetBool("isJumping", true);

        if (_inputValue == 0)
            _animator.SetBool("isRunning", false);
        else {
            if (_inputValue > 0 && _rb.transform.eulerAngles.y == 180)
                _rb.transform.eulerAngles = Vector3.zero;
            else if (_inputValue < 0 && _rb.transform.eulerAngles.y == 0)
                _rb.transform.eulerAngles = new Vector3(0, 180, 0);
            _animator.SetBool("isRunning", true);
        }
    }

    public void SetSpeed(string name, float value) {
        if (name == "Speed")
            _speed = value;
        else if (name == "Jump")
            _jumpForce = value;
    }
}
