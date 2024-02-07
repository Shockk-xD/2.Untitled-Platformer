using System.Collections;
using UnityEngine;

public class MazePlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private GameObject _fruits;
    private Rigidbody2D _rb;

    [HideInInspector] public Vector2 direction = Vector2.zero;

    [Header("For IEnumerator")]
    [SerializeField] private GameObject[] _forDelete;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _rb.velocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Maze-Fruit") {
            Destroy(collision.gameObject);
            if (_fruits.transform.childCount - 1 == 0) {
                StartCoroutine(ActionAfterWin());
            }
        }
    }

    private IEnumerator ActionAfterWin() {
        yield return new WaitForSeconds(2f);
        this.GetComponent<SpriteRenderer>().enabled = false;
        Vibrator.MediumVibration();
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _forDelete.Length; i++) {
            Destroy(_forDelete[i]);
            Vibrator.MediumVibration();
            yield return new WaitForSeconds(1f);
        }
    }
}