using UnityEngine;

public class Cloud9 : MonoBehaviour {
    [SerializeField] private float _speed = 2f;
    private float _distance = 4.85f;

    private void FixedUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.collider != null) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
                if (IsClose(hit.distance, _distance)) return;
                
                float currentDistance = hit.distance;
                float targetY = transform.position.y;

                if (currentDistance > _distance)
                    targetY -= _speed * Time.deltaTime;
                else
                    targetY += _speed * Time.deltaTime;

                targetY = Mathf.Clamp(targetY, 1.25f, float.MaxValue);
                transform.position = new Vector2(transform.position.x, targetY);
            }
        }
    }

    private bool IsClose(float a, float b) {
        return Mathf.Abs(a - b) < 0.5f;
    }
}
