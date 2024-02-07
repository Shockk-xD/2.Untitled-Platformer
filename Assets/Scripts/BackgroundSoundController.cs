using System.Collections;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _otherworldMusic;
    [SerializeField] private AudioClip _undergroundMusic;
    [SerializeField] private AudioClip _robertsTreeMusic;

    private AudioSource _backgroundMusicAudio;

    public static BackgroundSoundController instance;

    private void Start() {
        instance = this;
        _backgroundMusicAudio = GetComponent<AudioSource>();
        ChangeMusic(Music.Otherworld);
    }

    public enum Music {
        Otherworld,
        Underground,
        RobertsTree
    }

    public void ChangeMusic(Music music) {
        switch (music) {
            case Music.Otherworld:
                StartCoroutine(MusicAnimation(_otherworldMusic));
                break;
            case Music.Underground:
                StartCoroutine(MusicAnimation(_undergroundMusic));
                break;
            case Music.RobertsTree:
                StartCoroutine(MusicAnimation(_robertsTreeMusic));
                break;
        }
    }

    private IEnumerator MusicAnimation(AudioClip clip) {
        float timer = 0f;
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            _backgroundMusicAudio.volume = Mathf.Lerp(_backgroundMusicAudio.volume, 0, timer);
            return timer >= 1f;
        });

        _backgroundMusicAudio.clip = clip;
        _backgroundMusicAudio.Play();

        timer = 0f;
        yield return new WaitUntil(() => {
            timer += Time.deltaTime;
            _backgroundMusicAudio.volume = Mathf.Lerp(_backgroundMusicAudio.volume, 1, timer);
            return timer >= 1f;
        });
    }
}
