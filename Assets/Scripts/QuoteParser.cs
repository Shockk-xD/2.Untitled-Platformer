using System.Linq;
using TMPro;
using UnityEngine;

public class QuoteParser : MonoBehaviour
{
    [SerializeField] private TextMeshPro _quoteTMP;
    [SerializeField] private TextMeshPro _autorTMP;
    [SerializeField] private float _duration;
    private float _timer = 0;
    private string[] _quotes;
    private string[] _autors;

    private Animator _animator;

    private void Awake() {
        string path = "Quotes";
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null) {
            string[] lines = textAsset.text.Split('\n');
            _quotes = new string[lines.Length];
            _autors = new string[lines.Length];
            for (int i = 0; i < lines.Length; i++) {
                string quote = lines[i].Split(" -=- ").FirstOrDefault();
                string autor = lines[i].Split(" -=- ").LastOrDefault();
                _quotes[i] = quote;
                _autors[i] = autor;
            }
        }
    }

    private void Start() {
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        _timer += Time.deltaTime;
        
        if (_timer > _duration) {
            int randIndex = Random.Range(0, _quotes.Length);
            string quote = _quotes[randIndex];
            string autor = _autors[randIndex];

            _quoteTMP.text = $"\"{quote}\"";
            _autorTMP.text = $"- {autor}";

            _animator.SetTrigger("Open");

            _timer = 0;
        }
    }
}
