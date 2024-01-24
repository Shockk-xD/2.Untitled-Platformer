using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private GameObject _miniGamePrefab;
    [SerializeField] private float xCorrection;
    [SerializeField] private float yCorrection;
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
        Vector3 centerScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3((Screen.width / 2), (Screen.height / 2), 1));
        centerScreenPosition.x += xCorrection;
        centerScreenPosition.y += yCorrection;
        _miniGame = Instantiate(_miniGamePrefab, centerScreenPosition, Quaternion.identity);
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