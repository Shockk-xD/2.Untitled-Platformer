using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Text _woodsCount;
    [SerializeField] private ChestController _chestController;
    [SerializeField] private Animator _resourceQuantityAnimator;
    [SerializeField] private Animator _toolbarAnimator;
    [SerializeField] private Animator _inventoryAnimator;

    [Header("Hand Items")]
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private GameObject _handPickaxe;
    [Space(20)]

    private int _woods = 0;
    private bool _isInventoryOpen = false;
    private int _lastClickedToolbarSlot = 0;

    public GameObject[] toolbarSlots;
    public GameObject[] inventorySlots;
    public bool[] isToolbarFull;
    public bool[] isInventoryFull;

    private void Start() {
        ToolbarSlotClicked(_lastClickedToolbarSlot);
    }

    public void AddResources(string name, int count) {
        if (name == "woods")
            _woods += count;
        UpdateResourceQuantity();
    }

    public void AddItem(string name) {
        GameObject currentPrefab = null;
        if (name == "Pickaxe")
            currentPrefab = _chestController.pickaxePrefab;
        else if (name == "Ore")
            currentPrefab = _chestController.oreItemPrefab;

        bool isItemAdded = false;
        for (int i = 0; i < toolbarSlots.Length - 1; i++) {
            if (!isToolbarFull[i]) {
                Instantiate(currentPrefab, toolbarSlots[i].transform);
                isItemAdded = true;
                isToolbarFull[i] = true;
                break;
            }
        }
        if (isItemAdded) {
            if (!_toolbarAnimator.GetBool("ToolbarOpen"))
                _toolbarAnimator.SetBool("ToolbarOpen", true);
            RefreshToolbar(_lastClickedToolbarSlot);
            return;
        }
        for (int i = 0; i <  inventorySlots.Length; i++) {
            if (!isInventoryFull[i]) {
                Instantiate(currentPrefab, inventorySlots[i].transform);
                isInventoryFull[i] = true;
                break;
            }
        }
        RefreshToolbar(_lastClickedToolbarSlot);
    }

    private void UpdateResourceQuantity() {      
        _woodsCount.text = "" + _woods;
        if (_woods > 0 && !_resourceQuantityAnimator.GetBool("ItemOpen"))
            _resourceQuantityAnimator.SetBool("ItemOpen", true);
        else if (_woods == 0 && _resourceQuantityAnimator.GetBool("ItemOpen"))
            _resourceQuantityAnimator.SetBool("ItemOpen", false);
    }

    public void UpdateItemInHand() {
        if (_playerHand.transform.childCount == 1)
            Destroy(_playerHand.transform.GetChild(0).gameObject);
        if (toolbarSlots[_lastClickedToolbarSlot].transform.childCount == 1) {
            GameObject item = toolbarSlots[_lastClickedToolbarSlot].transform.GetChild(0).gameObject;
            item.name = item.name.Replace("(Clone)", "").Trim();

            if (_playerHand.transform.childCount == 0) {
                if (item.name == "Pickaxe Item")
                    Instantiate(_handPickaxe, _playerHand.transform);
            }
        }
    }

    private void RefreshToolbar(int i) {
        for (int j = 0; j < toolbarSlots.Length - 1; j++) {
            // Изменение прозрачности слота
            Color color = toolbarSlots[j].GetComponent<Image>().color;
            if (i != j)
                color = new Color(color.r, color.g, color.b, 200f / 255f);
            else
                color = new Color(color.r, color.g, color.b, 1f);
            toolbarSlots[j].GetComponent<Image>().color = color;

            // Изменение прозрачности предмета в слоте
            if (toolbarSlots[j].transform.childCount == 1) {
                Transform item = toolbarSlots[j].transform.GetChild(0);
                if (i != j)
                    item.GetComponent<Image>().color = new Color(1, 1, 1, 200f / 255f);
                else
                    item.GetComponent<Image>().color = Color.white;
            }
        }
        UpdateItemInHand();
    }

    public void ToolbarSlotClicked(int i) {
        if (_isInventoryOpen && _lastClickedToolbarSlot == i && toolbarSlots[i].transform.childCount == 1) {
            for (int j = 0; j < inventorySlots.Length; j++) {
                if (!isInventoryFull[j]) {
                    GameObject item = toolbarSlots[i].transform.GetChild(0).gameObject;
                    item.GetComponent<Image>().color = Color.white;
                    Instantiate(item, inventorySlots[j].transform);
                    Destroy(item.gameObject);
                    isInventoryFull[j] = true;
                    isToolbarFull[i] = false;
                    break;
                }
            }
        }
        _lastClickedToolbarSlot = i;
        RefreshToolbar(_lastClickedToolbarSlot);
    }

    public void InventorySlotClicked(int i) {
        if (inventorySlots[i].transform.childCount == 1) {
            for (int j = 0; j < toolbarSlots.Length - 1; j++) {
                if (!isToolbarFull[j]) {
                    GameObject item = inventorySlots[i].transform.GetChild(0).gameObject;
                    Instantiate(item, toolbarSlots[j].transform);
                    RefreshToolbar(_lastClickedToolbarSlot);
                    Destroy(item);
                    isToolbarFull[j] = true;
                    isInventoryFull[i] = false;
                    break;
                }
            }
        }
    }

    public void OpenInventory() {
        _isInventoryOpen = !_isInventoryOpen;
        Color buttonColor = toolbarSlots[3].GetComponent<Image>().color;
        if (_isInventoryOpen)
            buttonColor = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 1f);
        else
            buttonColor = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 200f / 255f);
        toolbarSlots[3].GetComponent<Image>().color = buttonColor;

        _inventoryAnimator.SetBool("InventoryOpen", _isInventoryOpen);
    }

    public bool CheckResources(string name, int value) {
        int count = 0;
        for (int i = 0; i < toolbarSlots.Length; i++) {
            if (toolbarSlots[i].transform.GetChild(0).gameObject.name.Replace("(Clone)", "").Trim() == name) {
                count++;
                if (count == value) {
                    return true;
                }
            }
        }
        for (int i = 0; i < inventorySlots.Length; i++) {
            if (inventorySlots[i].transform.GetChild(0).gameObject.name.Replace("(Clone)", "").Trim() == name) {
                count++;
                if (count == value) {
                    return true;
                }
            }
        }
        return false;
    }
}
