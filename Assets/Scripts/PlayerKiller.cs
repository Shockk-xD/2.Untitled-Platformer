using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    [SerializeField] private string _deathLocation;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            StartCoroutine(collision.GetComponent<Player>().Respawn(_deathLocation));
    }
}
