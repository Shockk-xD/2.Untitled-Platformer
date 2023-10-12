using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private PlayerInventory _inventory;

    private SpriteRenderer _sprite;
    private float _colorAlpha;

    private void Start() {
        _sprite = GetComponent<SpriteRenderer>();
        _particles = GetComponent<ParticleSystem>();
        _colorAlpha = _sprite.color.a;
    }

    private void OnMouseDown() {
        if (_hand.transform.childCount == 0) return;
        _particles.Play();
        //Debug.Log("Player has an item");
        GameObject itemInHand = _hand.transform.GetChild(0).gameObject;
        if (itemInHand.name.Replace("(Clone)", "").Trim() != "HandPickaxe") return;
        //Debug.Log("Item is pickaxe");
        _colorAlpha -= 50f / 255;
        Color color = _sprite.color;
        _sprite.color = new Color(color.r, color.g, color.b, _colorAlpha);
        if (_colorAlpha < 150f / 255) {
            Destroy(this.gameObject);
            _inventory.AddItem("Ore");
        }
    }
}
