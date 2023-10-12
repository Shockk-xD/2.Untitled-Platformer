using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;

    private ChestController _chestController;
    private Transform _item;

    private void Start() {
        _chestController = this.GetComponent<ChestController>();
    }

    public void SlotClicked(int index) {
        if (_chestController.slots[index].transform.childCount != 0)
            _item = this.GetComponent<ChestController>().slots[index].transform.GetChild(0);
        if (_item != null) {
            if (_item.name.Replace("(Clone)", "").Trim() == "Wood Item") {
                _inventory.AddResources("woods", 15);
                Destroy(_item.gameObject);
            } else if (_item.name.Replace("(Clone)", "").Trim() == "Pickaxe Item") {
                _inventory.AddItem("Pickaxe");
                Destroy(_item.gameObject);
            }
        }
    }
}
    