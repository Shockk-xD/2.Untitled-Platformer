using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    [SerializeField] protected Animator _inventoryAnimator;
    [SerializeField] private Animator _toolbarAnimator;

    private static Toolbar _instance;
    private ToolbarUI _toolbarUI;
    protected Inventory _inventory;

    public GameObject[] slots;

    protected Toolbar() { }

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        _toolbarUI = ToolbarUI.GetToolbarUI();
        _inventory = Inventory.GetInventory();
    }

    public static Toolbar GetToolbar() => _instance;

    public bool TryAddItem(GameObject itemPrefab) {
        if (!_toolbarAnimator.GetBool("ToolbarIsOpen")) {
            _toolbarAnimator.SetBool("ToolbarIsOpen", true);
        }

        for (int i = 0; i < slots.Length - 1; i++) {
            if (slots[i].transform.childCount == 0) {
                var item = Instantiate(itemPrefab, slots[i].transform);
                item.name = item.name.Replace("(Clone)", "").Trim();
                _toolbarUI.RefreshToolbar();
                StartCoroutine(_toolbarUI.UpdateItemInHand());
                return true;
            }
        }
        return false;
    }

    public bool TryRemoveItem(string name, int count) {
        if (GetItemCount(name) < count) return false;

        for (int i = 0; i < slots.Length - 1; i++) {
            if (slots[i].transform.childCount == 0) continue;
            var item = slots[i].transform.GetChild(0);
            if (item.name == name) {
                Destroy(item.gameObject);
                count--;
            }
            if (count == 0) return true;
        }

        return true;
    }

    public int GetItemCount(string name) {
        int count = 0;
        
        for (int i = 0; i < slots.Length - 1; i++) {
            if (slots[i].transform.childCount == 1) {
                var itemName = slots[i].transform.GetChild(0).name;
                if (itemName == name) count++;
            }
        }

        return count;
    }
}