using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Объекты, которые до запуска игры выключены, но нужно включить во время запуска")]
    [SerializeField] private GameObject _chestUI;
    [SerializeField] private GameObject _inventoryUI;

    [Header("Панелька. Для анимации при заходе в эту сцену")]
    [SerializeField] private Image _panel;

    private void Start() {
        StartCoroutine(UnfadePanel());
        _chestUI.SetActive(true);
        _inventoryUI.SetActive(true);
    }

    private IEnumerator UnfadePanel() {
        float timer = 0;

        yield return new WaitUntil(() => {
            Color startColor = new Color(0, 0, 0, 1);
            Color destinyColor = new Color(0, 0, 0, 0);
            timer += Time.deltaTime;

            _panel.color = Color.Lerp(startColor, destinyColor, timer / 3f);

            return timer >= 3f;
        });
    }
}
