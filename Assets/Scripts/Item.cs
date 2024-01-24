using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private GameObject _itemUI;

    private FloatingButtonController _fb;
    private Player _player;

    private void Start() {
        _fb = GetComponent<FloatingButtonController>();
        _fb.onClick = MoveToInventory;
        _player = Player.GetPlayer();
    }

    private void MoveToInventory() {
        if (_player.TryAddItem(_itemUI))
            Destroy(gameObject);
    }
}
