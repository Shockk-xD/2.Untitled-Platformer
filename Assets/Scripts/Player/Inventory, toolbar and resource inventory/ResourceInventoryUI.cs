using UnityEngine;
using UnityEngine.UI;

public class ResourceInventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _woods;

    private ResourceInventory _resourceInventory;
    private static ResourceInventoryUI _instance;

    private ResourceInventoryUI() { }

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        _resourceInventory = ResourceInventory.GetResourceInventory();
    }

    public static ResourceInventoryUI GetInstance() => _instance;

    public void UpdateUI() {
        _woods.GetComponentInChildren<Text>().text = "" + _resourceInventory.GetResourceCount(ResourceInventory.Resource.Wood);
    }
}
