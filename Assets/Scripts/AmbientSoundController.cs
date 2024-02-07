using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    public static AmbientSoundController instance;

    private AudioSource _ambientAudio;

    [SerializeField] private AudioClip _chestOpenClip;
    [SerializeField] private AudioClip _oreBrokenClip;

    private void Start() {
        instance = this;
        _ambientAudio = GetComponent<AudioSource>();
    }

    public enum Sound {
        ChestOpen,
        OreBroken,
        PlayerDeath
    }

    public void PlaySound(Sound sound) {
        switch (sound) {
            case Sound.ChestOpen:
                _ambientAudio.PlayOneShot(_chestOpenClip, 1f); break;
            case Sound.OreBroken:
                _ambientAudio.PlayOneShot(_oreBrokenClip, 0.15f); break;
        }
    }
}
