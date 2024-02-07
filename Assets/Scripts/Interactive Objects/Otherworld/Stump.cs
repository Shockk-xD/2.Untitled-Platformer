using UnityEngine;

public class Stump : MonoBehaviour
{
    private FloatingButtonController _fbController;
    private Animator _animator;
    private Player _player;

    private void Start() {
        _fbController = GetComponent<FloatingButtonController>();
        _animator = GetComponentInParent<Animator>();
        _player = Player.GetPlayer();
        _fbController.isErrorPresent = true;
        _fbController.onClick = Place;
    }

    private void Place() {
        int woodsCount = _player.GetResourceCount(ResourceInventory.Resource.Wood);
        if (woodsCount < 4) return;

        _fbController.isErrorPresent = false;
        _player.TryRemoveResource(ResourceInventory.Resource.Wood, 4);
        _animator.SetTrigger("StumpOn");
        _fbController.enabled = false;
        this.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == null || !collision.CompareTag("Player")) return;

        _animator.SetBool("IsHighlighting", true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == null || !collision.CompareTag("Player")) return;

        _animator.SetBool("IsHighlighting", false);
    }
}