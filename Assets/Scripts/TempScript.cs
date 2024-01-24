using UnityEngine;

public class TempScript : MonoBehaviour
{
    public void TeleportPlayerToGate() {
        Player.GetPlayer().transform.position = new Vector2(148.3f, 3.1f);
    }
}
