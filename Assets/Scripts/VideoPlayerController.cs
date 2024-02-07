using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer _videoPlayer;
    private float timer = 0;

    private void Start() {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update() {
        if (!_videoPlayer.isPlaying && timer > 3f) {
            SceneManager.LoadScene(0);
            return;
        }
        timer += Time.deltaTime;
    }
}
