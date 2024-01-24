using UnityEngine;
using UnityEngine.UI;

public class ResourceInventory : MonoBehaviour
{
    [SerializeField] private Animator _woodsUIAnimator;
    private static ResourceInventory _instance;
    private ResourceInventoryUI _inventoryUI;

    public int Woods;

    public enum Resource {
        Wood
    };

    private ResourceInventory() { }

    private void Awake() {
        _instance = this;
    }

    private void Start() {
        _inventoryUI = ResourceInventoryUI.GetInstance();
    }

    public static ResourceInventory GetResourceInventory() => _instance;

    public void AddResource(Resource resource, int count) {
        switch (resource) {
            case Resource.Wood:
                Woods += count;
                /*Text textComponent = _woodsUIAnimator.GetComponentInChildren<Text>();
                textComponent.text = Woods.ToString();*/
                bool woodsUIIsOpen = _woodsUIAnimator.GetBool("WoodsUIIsOpen");
                if (!woodsUIIsOpen)
                    _woodsUIAnimator.SetBool("WoodsUIIsOpen", true);
                break;
        }
        _inventoryUI.UpdateUI();
    }

    public bool TryRemoveResource(Resource resource, int count) {
        switch (resource) {
            case Resource.Wood:
                if (Woods >= count) {
                    Woods -= count;
                    _inventoryUI.UpdateUI();
                    return true;
                }
                break;
        }
        return false;
    }

    public int GetResourceCount(Resource resource) {
        return resource switch {
            Resource.Wood => Woods,
            _ => 0
        };
    }
}