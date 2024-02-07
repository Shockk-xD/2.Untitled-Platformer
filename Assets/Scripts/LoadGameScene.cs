using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameScene : MonoBehaviour
{
    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private Image _panel;

    private bool _isClicked = false;

    public void OnPlayButtonClick() {
        if (_isClicked) return;
        _isClicked = true;
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene() {
        float timer = 0;
        Color startColor = new Color(0, 0, 0, 0);
        Color destinyColor = new Color(0, 0, 0, 1);
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;

            _backgroundMusic.volume = Mathf.Lerp(0.2f, 0, timer / 2f);
            _panel.color = Color.Lerp(startColor, destinyColor, timer / 2f);

            return timer >= 2f;
        });
        SceneManager.LoadScene(1);
    }
}