using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBossfightScene : MonoBehaviour
{
    [SerializeField] private Image _panel;

    private bool _isLoading = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (_isLoading) return;
            _isLoading = true;
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene() {
        float timer = 0;
        Color destinyColor = new Color(0, 0, 0, 1);

        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            _panel.color = Color.Lerp(_panel.color, destinyColor, timer / 3f);
            return timer >= 3;
        });

        SceneManager.LoadScene(2);
    }
}
