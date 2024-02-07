using System.Collections;
using UnityEngine;

public class SpeedStaff : MonoBehaviour
{
    private Player _player;

    private void Start() {
        _player = Player.GetPlayer();
        _player.Controller.Speed = 6;
        StartCoroutine(CheckForSpeed());
    }

    private IEnumerator CheckForSpeed() {
        while (true) {
            if (_player.Controller.Speed != 6)
                _player.Controller.Speed = 6;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDestroy() {
        _player.Controller.Speed = 3;
    }
}
