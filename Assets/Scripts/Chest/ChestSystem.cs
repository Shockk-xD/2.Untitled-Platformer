using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _itemPrefabs = new GameObject[4];
    [SerializeField] private GameObject _chest;

    private Player _player;
    private Animator _chestUIAnimator;
    private FloatingButtonController _floatingButtonController;
    private ChestUI _chestUI;
    
    private void Start() {
        _player = Player.GetPlayer();
        _chestUI = _chest.GetComponent<ChestUI>();
        _chestUI._chestSlotOnClickAction = SlotClicked;
        _chestUIAnimator = _chest.GetComponent<Animator>();
        _floatingButtonController = GetComponent<FloatingButtonController>();
        _floatingButtonController.onClick = OpenChest;
        _floatingButtonController.triggerExitAction = CloseChest;
    }

    private void FillChest() {
        for (int i = 0; i < 4; i++) {
            if (_itemPrefabs[i] != null) {
                var item = Instantiate(_itemPrefabs[i], _chestUI.slots[i].transform);
                item.name = item.name.Replace("(Clone)", "").Trim();
            }
        }
    }

    private void OpenChest() {
        FillChest();
        _chestUIAnimator.SetBool("ChestIsOpen", true);
    }

    private void CloseChest() {
        _chestUIAnimator.SetBool("ChestIsOpen", false);
        ClearChest();
    }

    private void ClearChest() {
        for (int i = 0; i < _chestUI.slots.Length; i++) {
            Transform item = null;
            if (_chestUI.slots[i].transform.childCount == 1)
                item = _chestUI.slots[i].transform.GetChild(0);
            if (item != null)
                Destroy(item.gameObject);
        }
    }

    private void SlotClicked(int index) {
        if (_itemPrefabs[index] == null) return;
        var item = _chestUI.slots[index].transform.GetChild(0); 
        if (item.CompareTag("Resource")) {
            string name = item.name;
            if (name == "Wood Item") {
                int count = int.Parse(item.GetComponentInChildren<Text>().text);
                _player.AddResource(ResourceInventory.Resource.Wood, count);
                Destroy(item.gameObject);
            }
            _itemPrefabs[index] = null;
        } else if (item.CompareTag("Item")) {
            if (_player.TryAddItem(_itemPrefabs[index])) {
                _itemPrefabs[index] = null;
                Destroy(item.gameObject);
            }
        }
    }
}
