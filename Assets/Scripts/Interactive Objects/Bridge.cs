using UnityEngine;

public class Bridge : MonoBehaviour
{
    private Player _player;
    private FloatingButtonController _fbController;
    private Animator _animator;

    private void Start() {
        _player = Player.GetPlayer();
        _fbController = GetComponent<FloatingButtonController>();
        _animator = GetComponentInChildren<Animator>();
        _fbController.isErrorPresent = true;
        _fbController.onClick = BuildBridge;
    }

    private void BuildBridge() {
        var oreCount = _player.GetItemCount("Ore Item");
        var woodCount = _player.GetResourceCount(ResourceInventory.Resource.Wood);
        if (oreCount < 3 || woodCount < 10) return;

        _fbController.isErrorPresent = false;
        _player.TryRemoveItem("Ore Item", 3);
        _player.TryRemoveResource(ResourceInventory.Resource.Wood, 10);
        _animator.SetTrigger("BridgeOn");
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
