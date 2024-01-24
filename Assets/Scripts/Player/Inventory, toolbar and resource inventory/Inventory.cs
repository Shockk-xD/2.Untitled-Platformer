using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;

    public GameObject[] slots;
   
    protected Inventory() { }

    public void Awake() {
        _instance = this;
    }

    public static Inventory GetInventory() => _instance;

    public bool TryAddItem(GameObject itemPrefab) {
        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].transform.childCount == 0) {
                var item = Instantiate(itemPrefab, slots[i].transform);
                item.name = item.name.Replace("(Clone)", "").Trim();
                return true;
            }
        }
        return false;
    }

    public int GetItemCount(string name) {
        int count = 0;

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].transform.childCount == 1) {
                var itemName = slots[i].transform.GetChild(0).name;
                if (itemName == name) count++;
            }
        }

        return count;
    }

    public void TryRemoveItem(string name, int count) {
        if (GetItemCount(name) < count) return;

        for (int i = 0; i < slots.Length; i++) {
            if (slots[i].transform.childCount == 0) continue;
            var item = slots[i].transform.GetChild(0);
            if (item.name == name) {
                Destroy(item.gameObject);
                count--;
            }
            if (count == 0) return;
        }
    }
}