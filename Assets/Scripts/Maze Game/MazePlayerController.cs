using Unity.VisualScripting;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject fruits;

    private MazeGameController _playerDirection;
    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _playerDirection = GameObject.Find("GameController").GetComponent<MazeGameController>();
    }

    private void Update() {
        _rb.velocity = _playerDirection.direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Maze-Fruit") {
            Destroy(collision.gameObject);
            if (fruits.transform.childCount - 1 == 0) {
                _playerDirection.ChangeGameStatus(true);
            }
        }
    }
}
