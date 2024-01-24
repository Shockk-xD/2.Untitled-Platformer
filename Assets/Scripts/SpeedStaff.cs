using UnityEngine;

public class SpeedStaff : MonoBehaviour
{
    private Player _player;

    private void Start() {
        _player = Player.GetPlayer();
        _player.Controller.SetSpeed(6);
    }

    private void OnDestroy() {
        _player.Controller.SetSpeed(3);
    }
}
