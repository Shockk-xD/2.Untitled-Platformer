using System.Collections;
using UnityEngine;

public class JumpStaff : MonoBehaviour
{
    private Player _player;

    private void Start() {
        _player = Player.GetPlayer();
        _player.Controller.JumpForce = 6;

        StartCoroutine(CheckForJump());
    }

    private IEnumerator CheckForJump() {
        while (true) {
            if (_player.Controller.JumpForce != 6)
                _player.Controller.JumpForce = 6;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnDestroy() {
        _player.Controller.JumpForce = 4;
    }
}
