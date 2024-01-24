using UnityEngine;

public class JumpStaff : MonoBehaviour
{
    private Player _player;

    private void Start() {
        _player = Player.GetPlayer();
        _player.Controller.SetJumpForce(6);
    }

    private void OnDestroy() {
        _player.Controller.SetJumpForce(4);
    }
}
