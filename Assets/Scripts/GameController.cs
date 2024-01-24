using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Объекты, которые до запуска игры выключены, но нужно включить во время запуска")]
    [SerializeField] private GameObject _chestUI;
    [SerializeField] private GameObject _inventoryUI;

    private void Start() {
        _chestUI.SetActive(true);
        _inventoryUI.SetActive(true);
    }
}
