using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed { get; set; } = 3;
    public float JumpForce { get; set; } = 4;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _playerFeetPosition;

    [SerializeField] private AudioSource _playerAudio;
    [SerializeField] private AudioSource _jumpAudio;
    [SerializeField] private AudioClip _jumpClip;

    [SerializeField] private Joystick _joystick;

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
        CheckJump();
    }

    private void Run() {
        _inputValue = _joystick.Horizontal;
        _rb.velocity = new Vector2(_inputValue * Speed, _rb.velocity.y);


        if (_inputValue > 0 && _rb.transform.eulerAngles.y == 180) {
            _rb.transform.eulerAngles = Vector3.zero;
        } else if (_inputValue < 0 && _rb.transform.eulerAngles.y == 0) {
            _rb.transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (_inputValue != 0) {
            float randPitch = Random.Range(1f, 1.5f);
            _playerAudio.pitch = randPitch;
        }
        _playerAudio.volume = Mathf.Abs(_inputValue);
        _playerAudio.mute = !_isGrounded;

        _animator.SetBool("IsRunning", _inputValue != 0);
    }

    private void CheckJump() {
        _isGrounded = Physics2D.OverlapCircle(_playerFeetPosition.transform.position, 0.3f, _groundLayer);
        _animator.SetBool("IsJumping", !_isGrounded);
    }

    public void Jump() {
        if (_isGrounded) {
            _rb.velocity = Vector2.up * JumpForce;
            _animator.SetTrigger("TakeOff");

            _jumpAudio.PlayOneShot(_jumpClip);
        }
    }
}