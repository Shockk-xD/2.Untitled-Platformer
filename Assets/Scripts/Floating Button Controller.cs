using UnityEngine;
using UnityEngine.UI;

public class FloatingButtonController : MonoBehaviour
{
    [SerializeField, TextArea(1, 2)] private string _floatingButtonString;
    [SerializeField] private ChestController _chestController;
    [SerializeField] private FloatingButtonSettings _settings;
    [SerializeField] private GameObject _chest;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _floatingButtonText;

    private Transform _slotItem;
    private bool _isHaveResources = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer != 7) return;
        _animator.SetBool("StartOpen", true);
        _floatingButtonText.text = _floatingButtonString;
        if (this.tag == "Chest") {
            if (this.name == "Chest 1") {
                _settings.action = "ChestOpen";
                if (_chestController.firstChest) {
                    _chestController.lastChest = "Chest 1";
                    _chestController.canOpenChest = true;
                } else {
                    _chestController.canOpenChest = false;
                    _floatingButtonText.text = "Сундук пуст!";
                }
            } else if (this.name == "Chest 2") {
                _settings.action = "Maze";
                if (_chestController.secondChest) {
                    _chestController.lastChest = "Chest 2";
                    _chestController.canOpenChest = true;
                } else {
                    _chestController.canOpenChest = false;
                    _floatingButtonText.text = "Сундук пуст!";
                }
            }
        } else if (this.name == "Tree Trigger") {
            _settings.action = "Stump";
            GameObject.Find("Wood-Stump").GetComponent<Animator>().SetBool("isHighlighting", true);
        } else if (this.name == "Bridge") {
            if (!_isHaveResources) {
                _floatingButtonText.text = "Не хватает ресурсов для постройки моста (нужны 3 руды и 25 дерева)";
                _settings.action = "";
            } else {
                _floatingButtonText.text = "Нажмите на кнопку, чтобы построить мост";
                _settings.action = "Bridge";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        _animator.SetBool("StartOpen", false);
        if (this.tag == "Chest") {
            if (_chest != null)
                _chest.GetComponent<Animator>().SetBool("ChestOpen", false);
        } else if (this.gameObject.name == "Tree Trigger")
            GameObject.Find("Wood-Stump").GetComponent<Animator>().SetBool("isHighlighting", false);
        GameObject.Find("Panel").GetComponent<Animator>().SetBool("isOn", false);
        for (int i = 0; i < _chestController.slots.Length; i++) {
            if (_chestController.slots[i].transform.childCount != 0)
                _slotItem = _chestController.slots[i].transform.GetChild(0);
            if (_slotItem != null)
                Destroy(_slotItem.gameObject);
        }
    }
}
    