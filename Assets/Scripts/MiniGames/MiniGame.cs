using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private GameObject _miniGamePrefab;
    [SerializeField] private Vector2 _spawnPosition;
    [SerializeField] private Component _component; 
    private FloatingButtonController _floatingButtonController;
    private Collider2D _collider;
    private GameObject _miniGame;

    private void Start() {
        _collider = GetComponent<Collider2D>();
        _floatingButtonController = GetComponent<FloatingButtonController>();
        _floatingButtonController.onClick = Summon;
    }

    private void Summon() {
        _miniGame = Instantiate(_miniGamePrefab, _spawnPosition, Quaternion.identity);
        _collider.enabled = false;
    }

    private void Update() {
        if (_miniGame == null && !_collider.enabled) {
            if (_component is Behaviour behaviour) {
                behaviour.enabled = true;
            }
            _collider.enabled = true;
            Destroy(this);
        }
    }
}