using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GateToUnderground : MonoBehaviour
{
    [SerializeField] private GameObject _floor;
    [SerializeField] private GameObject _robertsTree;
    [SerializeField] private Image _panel;
    [SerializeField] private GameObject _player;
    [SerializeField] private CinemachineConfiner2D _confiner;
    [SerializeField] private Collider2D _mapCollider;
    private FloatingButtonController _controller;

    private bool _panelIsEnabled = false;
    private bool _panelTriggerOn = false;
    private readonly Color _panelNormalColor = new Color(0, 0, 0, 0);
    private readonly Color _panelDestinyColor = new Color(0, 0, 0, 1);
    private float _timer = 0;

    private void Start() {
        _controller = GetComponent<FloatingButtonController>();
        _controller.onClick = OnButtonClick;
    }

    private void OnButtonClick() {
        Destroy(_floor);
        Destroy(_robertsTree);
        StartCoroutine(TeleportToUnderground());
    }

    private IEnumerator TeleportToUnderground() {
        _panelTriggerOn = true; // Вкл. черного экрана
        yield return new WaitUntil(() => !_panelTriggerOn);
        _player.transform.position = new Vector2(145.3f, -11.81f);
        _confiner.m_BoundingShape2D = _mapCollider;
        Camera.main.backgroundColor = new Color(39 / 255f, 39 / 255f, 39 / 255f);
        _panelTriggerOn = true;
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
