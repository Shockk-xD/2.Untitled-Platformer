using UnityEngine;

public class MazeGameController : MonoBehaviour
{
    [SerializeField] private ChestController _chestController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _buttons;
    private bool _isGameWon = false;

    [HideInInspector] public Vector2 direction;

    private void Start() {
        direction = Vector2.zero;
    }

    public void ChangeGameStatus(bool isGameWon) {
        _isGameWon = isGameWon;
        if (_isGameWon) {
            Destroy(GameObject.Find("Maze(Clone)"));
            _buttons.SetActive(false);
            _chestController.canOpenChest = true;
            _playerController.SetSpeed("Speed", 3);
            _playerController.SetSpeed("Jump", 4);
            _chestController.OpenChest();
        }
    }

    public void ChangeDirection(string name) {
        if (name == "Up")
            direction = Vector2.up;
        else if (name == "Down")
            direction = Vector2.down;
        else if (name == "Right")
            direction = Vector2.right;
        else if (name == "Left")
            direction = Vector2.left;
    }
}
