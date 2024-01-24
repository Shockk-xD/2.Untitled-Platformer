using UnityEngine;

public class MazeButton : MonoBehaviour
{
    [SerializeField] private MazePlayerController _playerController;
    [SerializeField] private Vector2 _direction;

    private bool _playerOnButton = false;
    private SpriteRenderer _spriteRenderer;
    private Color _normalColor = new Color(1f, 1f, 1f, 100f / 255f);
    private Color _highlitedColor = new Color(1f, 1f, 1f, 1f);

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (_playerOnButton) {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _highlitedColor, Time.deltaTime / 0.5f);
        } else {
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _normalColor, Time.deltaTime / 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;
        _playerOnButton = true;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;
        _playerController.direction = _direction;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;
        _playerOnButton = false;
        _playerController.direction = Vector2.zero;
    }
}
