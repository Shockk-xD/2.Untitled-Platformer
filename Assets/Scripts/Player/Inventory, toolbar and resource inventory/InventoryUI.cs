using UnityEngine;

public class InventoryUI : Inventory
{
    private Toolbar _toolbar;

    private void Start() {
        _toolbar = Toolbar.GetToolbar();
    }

    public void OnClick(int i) {
        if (slots[i].transform.childCount == 0) return;

        var item = slots[i].transform.GetChild(0);
        if (_toolbar.TryAddItem(item.gameObject))
            Destroy(item.gameObject);
    }
}
