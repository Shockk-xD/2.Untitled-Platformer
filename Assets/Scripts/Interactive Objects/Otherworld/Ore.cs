using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private GameObject _playerHand;
    [SerializeField] private GameObject _oreAsItem;
    private Player _player;
    private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;
    private float _colorAlpha;

    private void Start() {
        this.GetComponent<CircleCollider2D>().radius = 0.625f;
        _player = Player.GetPlayer();
        _particleSystem = GetComponent<ParticleSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colorAlpha = _spriteRenderer.color.a;
    }

    private void OnMouseDown() {
        _particleSystem.Play();
        Vibrator.MediumVibration();
        if (!(_playerHand.transform.childCount == 1 && _playerHand.transform.GetChild(0).name.Replace("(Clone)", "").Trim() == "Pickaxe Item (Hand)")) return;
        AmbientSoundController.instance.PlaySound(AmbientSoundController.Sound.OreBroken);
        _colorAlpha -= 50f / 255f;
        Color color = _spriteRenderer.color;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, _colorAlpha);
        if (_colorAlpha < 150f / 255f) {
            _player.TryAddItem(_oreAsItem);
            Destroy(this.gameObject);
        }
    }
}
