using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarUI : Toolbar
{
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private GameObject[] _itemsToHand;
    private int _lastClickedSlot;
    private static ToolbarUI _instance;

    private ToolbarUI() { }

    private void Awake() {
        _instance = this;
    }

    public static ToolbarUI GetToolbarUI() => _instance;

    public void OnClick(int index) {
        bool inventoryIsOpen = _inventoryAnimator.GetBool("InventoryIsOpen");
        if (index == 3) {
            _inventoryAnimator.SetBool("InventoryIsOpen", !inventoryIsOpen);
            RefreshToolbar();
            return;
        }
        if (inventoryIsOpen && slots[index].transform.childCount == 1 && _lastClickedSlot == index) {
            var item = slots[index].transform.GetChild(0);
            if (_inventory.TryAddItem(item.gameObject))
                Destroy(item.gameObject);
        }
        _lastClickedSlot = index;
        RefreshToolbar();
        StartCoroutine(UpdateItemInHand());
    }

    public void RefreshToolbar() {
        var color = slots[0].GetComponent<Image>().color;
        for (int i = 0; i < slots.Length - 1; i++) {
            var slotImage = slots[i].GetComponent<Image>();

            if (i != _lastClickedSlot) {
                slotImage.color = new Color(color.r, color.g, color.b, 200f / 255f);
            } else {
                slotImage.color = new Color(color.r, color.g, color.b, 1f);
            }

            if (slots[i].transform.childCount == 1) {
                var item = slots[i].transform.GetChild(0).gameObject;
                if (i != _lastClickedSlot) {
                    item.GetComponent<Image>().color = new Color(1f, 1f, 1f, 200f / 255f);
                } else {
                    item.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }

        bool inventoryIsOpen = _inventoryAnimator.GetBool("InventoryIsOpen");
        if (!inventoryIsOpen) {
            slots[3].GetComponent<Image>().color = new Color(color.r, color.g, color.b, 200f / 255f);
        } else {
            slots[3].GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1f);
        }
    }

    public IEnumerator UpdateItemInHand() {
        yield return new WaitForSeconds(0.1f);
        if (_playerHand.transform.childCount == 1)
            Destroy(_playerHand.transform.GetChild(0).gameObject);
        if (slots[_lastClickedSlot].transform.childCount != 0) {
            var item = slots[_lastClickedSlot].transform.GetChild(0);
            for (int i = 0; i < _itemsToHand.Length; i++) {
                if (item.name == _itemsToHand[i].name.Replace("(Hand)", "").Trim()) {
                    var handItem = Instantiate(_itemsToHand[i], _playerHand.transform);
                    handItem.name = handItem.name.Replace("(Clone)", "").Trim();
                }
            }
        }
    }
}
