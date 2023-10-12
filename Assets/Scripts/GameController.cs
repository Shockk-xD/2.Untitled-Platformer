using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _chestInterface;
    [SerializeField] private GameObject _inventoryInterface;

    private void Start() {
        _chestInterface.SetActive(true);
        _inventoryInterface.SetActive(true);
    }
}
