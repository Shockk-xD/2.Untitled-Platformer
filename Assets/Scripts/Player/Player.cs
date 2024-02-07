using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Image _panel;

    public Inventory Inventory { get; private set; }
    public Toolbar Toolbar { get; private set; }
    public ResourceInventory Resources { get; private set; }
    public PlayerController Controller { get; private set; }

    public static Player _instance;

    private Player() { }

    private void Awake() {
        _instance = this;
        Controller = GetComponent<PlayerController>();
    }

    private void Start() {
        Inventory = Inventory.GetInventory();
        Toolbar = Toolbar.GetToolbar();
        Resources = ResourceInventory.GetResourceInventory();
    }

    public static Player GetPlayer() => _instance;

    public bool TryAddItem(GameObject item) {
        return Toolbar.TryAddItem(item) || Inventory.TryAddItem(item);
    }

    public bool TryRemoveItem(string name, int count) {
        if (GetItemCount(name) < count) return false;
        int countInToolbar = Mathf.Clamp(Toolbar.GetItemCount(name), 0, count);
        int countInInventory = count - countInToolbar;
        Toolbar.TryRemoveItem(name, countInToolbar);
        Inventory.TryRemoveItem(name, countInInventory);
        return true;
    }

    public int GetItemCount(string name) {
        int count = Toolbar.GetItemCount(name) + Inventory.GetItemCount(name);
        return count;
    }

    public void AddResource(ResourceInventory.Resource resource, int count) {
        Resources.AddResource(resource, count);
    }

    public bool TryRemoveResource(ResourceInventory.Resource resource, int count) {
        return Resources.TryRemoveResource(resource, count);
    }

    public int GetResourceCount(ResourceInventory.Resource resource) => Resources.GetResourceCount(resource);

    public IEnumerator Respawn(string deathLocation) {
        Text deathText = _panel.GetComponentInChildren<Text>();
        float timer = 0f;
        float duration = 1f;
        Color color = new Color(0.641f, 0.0326f, 0.0326f);
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            float t = timer / duration;
            _panel.color = Color.Lerp(_panel.color, new Color(0, 0, 0, 1), t);
            deathText.color = Color.Lerp(_panel.color, new Color(color.r, color.g, color.b, 1), t);
            return _panel.color == new Color(0, 0, 0, 1) && deathText.color == new Color(color.r, color.g, color.b, 1);
        });

        switch (deathLocation) {
            case "Underground":
                transform.position = new Vector2(145.3f, -11.81f);
                break;
            case "Islands":
                transform.position = new Vector2(266.46f, 1.91f);
                break;
            case "Islands2":
                transform.position = new Vector2(303.96f, 1.65f);
                break;
        }

        yield return new WaitForSeconds(1);
        timer = 0;
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            float t = timer / duration;
            _panel.color = Color.Lerp(_panel.color, new Color(0, 0, 0, 0), t);
            deathText.color = Color.Lerp(_panel.color, new Color(color.r, color.g, color.b, 0), t);
            return _panel.color == new Color(0, 0, 0, 0) && deathText.color == new Color(color.r, color.g, color.b, 0);
        });
    }
}
