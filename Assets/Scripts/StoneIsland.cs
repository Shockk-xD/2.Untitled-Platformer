using System.Collections;
using UnityEngine;

public class StoneIsland : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _latencyBeforeStart;
    private float _defaultY;

    private void Start() {
        _defaultY = transform.localPosition.y;
        StartCoroutine(AnimateStone());
    }
    
    private IEnumerator AnimateStone() {
        yield return new WaitForSeconds(_latencyBeforeStart);
        while (true) {
            while(transform.localPosition.y != -7) {
                transform.localPosition = Vector2.MoveTowards(
                    transform.localPosition,
                    new Vector2(transform.localPosition.x, -7),
                    Time.deltaTime * _moveSpeed);
                yield return null;
            }
            yield return new WaitForSeconds(1);
            while(transform.localPosition.y != _defaultY) {
                transform.localPosition = Vector2.MoveTowards(
                    transform.localPosition,
                    new Vector2(transform.localPosition.x, _defaultY),
                    Time.deltaTime * _moveSpeed);
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
