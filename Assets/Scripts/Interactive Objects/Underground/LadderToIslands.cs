using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LadderToIslands : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _clouds;
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private Collider2D _mapCollider;

    private bool _panelIsEnabled = false;
    private bool _panelTriggerOn = false;
    private readonly Color _panelNormalColor = new Color(0, 0, 0, 0);
    private readonly Color _panelDestinyColor = new Color(0, 0, 0, 1);
    private float _timer = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            StartCoroutine(TeleportToIslands());
    }

    public void Foo() {
        StartCoroutine(TeleportToIslands());
    }

    private IEnumerator TeleportToIslands() {
        _panelTriggerOn = true;
        BackgroundSoundController.instance.ChangeMusic(BackgroundSoundController.Music.Otherworld);
        yield return new WaitUntil(() => !_panelTriggerOn);
        _player.transform.position = new Vector2(266.46f, 1.91f);
        _confiner.m_BoundingShape2D = _mapCollider;
        Camera.main.backgroundColor = new Color(0, 219 / 255f, 226 / 255f);
        _panelTriggerOn = true;

        float tempTimer = 0;
        yield return new WaitWhile(() => {
            tempTimer += Time.deltaTime;

            _clouds.transform.position = new Vector2(_player.transform.position.x, 5.67f);

            return tempTimer < 0.5f;
        });


        yield return new WaitUntil(() => !_panelTriggerOn);
    }

    private void Update() {
        if (!_panelTriggerOn) return;

        Color colorTo = _panelIsEnabled ? _panelNormalColor : _panelDestinyColor;
        _timer += Time.deltaTime;
        float t = _timer / 3f;

        _panel.color = Color.Lerp(_panel.color, colorTo, t);

        if (_panel.color == colorTo) {
            _panelIsEnabled = !_panelIsEnabled;
            _panelTriggerOn = false;
            _timer = 0;
        }
    }
}
