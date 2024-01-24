using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _timer = 0;

    private Rigidbody2D _rb;
    private Vector2 startPosition;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        startPosition = new Vector2(transform.localPosition.x, 3);
    }

    private void Update() {
        if (_timer < _duration) {
            _timer += Time.deltaTime;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, startPosition, Time.deltaTime * 2f);
            return;
        }

        if (!_rb.simulated) _rb.simulated = true;

        if (transform.localPosition.y < -10) {
            _rb.simulated = false;
            _rb.velocity = Vector2.zero;
            transform.localPosition = new Vector2(transform.localPosition.x, 5.5f);
            _timer = 0;
        }
    }

    private void FixedUpdate() {
        _rb.velocity = new Vector2(0, Mathf.Clamp(_rb.velocity.y, -10, 0));
    }
}
