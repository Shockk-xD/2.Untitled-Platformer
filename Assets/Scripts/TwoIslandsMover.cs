using System.Collections;
using UnityEngine;

public class TwoIslandsMover : MonoBehaviour
{
    [SerializeField] private GameObject _island1;
    [SerializeField] private GameObject _island2;
    [SerializeField] private Vector3 _island1DestinyPosition;
    [SerializeField] private Vector3 _island2DestinyPosition;
    [SerializeField] private float _force;

    private Vector3 _island1StartPosition;
    private Vector3 _island2StartPosition;

    private Rigidbody2D _island1Rb;
    private Rigidbody2D _island2Rb;

    private void Start() {
        _island1Rb = _island1.GetComponent<Rigidbody2D>();
        _island2Rb = _island2.GetComponent<Rigidbody2D>();

        _island1StartPosition = _island1.transform.localPosition;
        _island2StartPosition = _island2.transform.localPosition;
        StartCoroutine(Move());
    }

    private IEnumerator Move() {
        while (true) {
            yield return new WaitUntil(() => {
                /*_island1.transform.localPosition = Vector3.MoveTowards(_island1.transform.localPosition,
                    _island1DestinyPosition,
                    Time.deltaTime * _speed
                    );
                _island2.transform.localPosition = Vector3.MoveTowards(_island2.transform.localPosition,
                    _island2DestinyPosition,
                    Time.deltaTime * _speed
                    );*/
                _island1Rb.velocity = Vector2.left * _force * Time.deltaTime;
                _island2Rb.velocity = Vector2.right * _force * Time.deltaTime;

                return _island1.transform.localPosition.x < _island1DestinyPosition.x &&
                       _island2.transform.localPosition.x > _island2DestinyPosition.x;
            });
            _island1Rb.velocity = Vector2.zero;
            _island2Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(3);
            yield return new WaitUntil(() => {
                /*_island1.transform.localPosition = Vector3.MoveTowards(_island1.transform.localPosition,
                    _island1StartPosition,
                    Time.deltaTime * _force
                    );
                _island2.transform.localPosition = Vector3.MoveTowards(_island2.transform.localPosition,
                    _island2StartPosition,
                    Time.deltaTime * _force
                    );*/
                _island1Rb.velocity = Vector2.right * _force * Time.deltaTime;
                _island2Rb.velocity = Vector2.left * _force * Time.deltaTime;

                return _island1.transform.localPosition.x > _island1StartPosition.x &&
                       _island2.transform.localPosition.x < _island2StartPosition.x;
            });
            _island1Rb.velocity = Vector2.zero;
            _island2Rb.velocity = Vector2.zero;
            yield return new WaitForSeconds(3);
        }
    }
}
