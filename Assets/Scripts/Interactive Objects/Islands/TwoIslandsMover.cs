using System.Collections;
using UnityEngine;

public class TwoIslandsMover : MonoBehaviour
{
    [SerializeField] private GameObject _island1;
    [SerializeField] private GameObject _island2;
    [SerializeField] private Vector3 _island1DestinyPosition;
    [SerializeField] private Vector3 _island2DestinyPosition;
    [SerializeField] private float _speed;

    private Vector3 _island1StartPosition;
    private Vector3 _island2StartPosition;

    private void Start() {
        _island1StartPosition = _island1.transform.localPosition;
        _island2StartPosition = _island2.transform.localPosition;
        StartCoroutine(Move());
    }

    private IEnumerator Move() {
        while (true) {
            yield return new WaitUntil(() => {
                _island1.transform.localPosition = Vector3.MoveTowards(_island1.transform.localPosition,
                    _island1DestinyPosition,
                    Time.deltaTime * _speed
                    );
                _island2.transform.localPosition = Vector3.MoveTowards(_island2.transform.localPosition,
                    _island2DestinyPosition,
                    Time.deltaTime * _speed
                    );

                return _island1.transform.localPosition.x == _island1DestinyPosition.x &&
                       _island2.transform.localPosition.x == _island2DestinyPosition.x;
            });
            yield return new WaitForSeconds(3);
            yield return new WaitUntil(() => {
                _island1.transform.localPosition = Vector3.MoveTowards(_island1.transform.localPosition,
                    _island1StartPosition,
                    Time.deltaTime * _speed
                    );
                _island2.transform.localPosition = Vector3.MoveTowards(_island2.transform.localPosition,
                    _island2StartPosition,
                    Time.deltaTime * _speed
                    );

                return _island1.transform.localPosition.x == _island1StartPosition.x &&
                       _island2.transform.localPosition.x == _island2StartPosition.x;
            });
            yield return new WaitForSeconds(3);
        }
    }
}
