using UnityEngine;
using UnityEngine.UI;

public class Quote : MonoBehaviour
{
    [SerializeField] private Text _quoteText;
    [SerializeField] private Text _autorText;

    public string quote = "Цитата";
    public string autor = "Автор";

    private SpriteRenderer _renderer;
    private float _timer = 5f;
    private int _alpha = 255;

    private void Start() {
        _quoteText.text = quote;
        _autorText.text = autor;
    }

    private void Update() {
        if (_timer > 0) {
            _timer -= Time.deltaTime;
            return;
        }

        Erase();
    }

    private void Erase() {
        if (_renderer.color.a > 1) {
            Color color = _renderer.color;
            _renderer.color = new Color(color.r, color.g, color.b, _alpha);

            color = _quoteText.color;
            _quoteText.color = new Color(color.r, color.g, color.b, _alpha);

            color = _autorText.color;
            _autorText.color = new Color(color.r, color.g, color.b, _alpha);
        } else
            Destroy(this.gameObject);
    }
}
