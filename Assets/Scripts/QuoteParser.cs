using UnityEngine;
using UnityEngine.UI;

public class QuoteParser : MonoBehaviour
{
    [SerializeField] private Animator _quoteParserAnimator;
    [SerializeField] private Text _quoteText;
    [SerializeField] private Text _quoteAutorText;
    [SerializeField] private float _timeToShowQuote = 30f;

    [Header("Цитаты")]
    [SerializeField] private string[] _quotes;
    [SerializeField] private string[] _quoteAutors;

    private float timer;

    private void Start() {
        timer = _timeToShowQuote;
    }

    private void Update() {
        if (timer > 0) 
            timer -= Time.deltaTime;
        else {
            int rand = Random.Range(0, _quotes.Length);
            _quoteText.text = $"\"{_quotes[rand]}\"";
            _quoteAutorText.text = $"- {_quoteAutors[rand]}";
            _quoteParserAnimator.SetTrigger("ShowQuote");
            timer = _timeToShowQuote;
        }
    }
}
