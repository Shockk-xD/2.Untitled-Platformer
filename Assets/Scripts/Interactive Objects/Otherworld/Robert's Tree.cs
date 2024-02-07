using UnityEngine;
using UnityEngine.UI;

public class RobertsTree : MonoBehaviour
{
    [SerializeField] private Image _panel;
    private bool _isFading = false;

    private Color _normalColor = new Color(0, 0, 0, 0);
    private Color _destinyColor = new Color(0, 0, 0, 200 / 255f);

    private float _timer = 0;
    private const float DURATION = 3f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _timer = 0;
            _isFading = true;
            BackgroundSoundController.instance.ChangeMusic(BackgroundSoundController.Music.RobertsTree);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            _timer = 0;
            _isFading = false;
            BackgroundSoundController.instance.ChangeMusic(BackgroundSoundController.Music.Otherworld);
        }
    }

    private void Update() {
        _timer += Time.deltaTime;
        float t = _timer / DURATION;
        if (_isFading)
            _panel.color = Color.Lerp(_panel.color, _destinyColor, t);
        else
            _panel.color = Color.Lerp(_panel.color, _normalColor, t);
    }
}