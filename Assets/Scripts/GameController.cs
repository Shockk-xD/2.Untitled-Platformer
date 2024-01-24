using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�������, ������� �� ������� ���� ���������, �� ����� �������� �� ����� �������")]
    [SerializeField] private GameObject _chestUI;
    [SerializeField] private GameObject _inventoryUI;

    private void Start() {
        _chestUI.SetActive(true);
        _inventoryUI.SetActive(true);
    }
}
