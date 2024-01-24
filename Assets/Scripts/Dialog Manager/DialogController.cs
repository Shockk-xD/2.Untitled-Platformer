using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [SerializeField] private Dialog _dialog;
    [SerializeField] private Text _dialogName;
    [SerializeField] private Text _dialogText;
    [SerializeField] private Animator _dialogAnimator;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _tradeShop;

    private FloatingButtonController _buttonController;
    private Queue<string> _dialogTexts;

    private void Start() {
        _buttonController = GetComponent<FloatingButtonController>();
        _buttonController.onClick = StartDialog;
        _dialogTexts = new Queue<string>();
    }

    private void StartDialog() {
        _buttonController.triggerExitAction = StopDialog;

        _dialogAnimator.SetBool("IsActive", true);
        _dialogTexts.Clear();
        foreach (var text in _dialog.texts)
            _dialogTexts.Enqueue(text);
        _dialogName.text = _dialog.name;

        Text tempComponent = _button.GetComponentInChildren<Text>();
        tempComponent.text = "Далее >>";
        tempComponent.color = new Color(50 / 255f, 50 / 255f, 50 / 255f);

        StartCoroutine(ShowNextSentence());
    }

    private System.Collections.IEnumerator ShowNextSentence() {
        _dialogText.text = "";
        string text = _dialogTexts.Dequeue();
        foreach (char c in text) {
            _dialogText.text += "" + c;
            yield return new WaitForSeconds(1.5f / text.Length);
        }
    }

    public void OnNextButtonClick() {
        if (_dialogTexts.Count > 0) {
            if (_dialogTexts.Count == 1) {
                Text tempComponent = _button.GetComponentInChildren<Text>();
                tempComponent.text = "Открыть магазин";
            }
            StopAllCoroutines();
            StartCoroutine(ShowNextSentence());
        } else {
            _tradeShop.GetComponent<Animator>().SetBool("IsOpen", true);
            Trader.isActive = true;
            _buttonController.triggerExitAction += () => {
                _tradeShop.GetComponent<Animator>().SetBool("IsOpen", false);
                Trader.isActive = false;
            };
            StopDialog();
        }
    }

    private void StopDialog() {
        StopAllCoroutines();
        if (_dialogAnimator != null)
            _dialogAnimator.SetBool("IsActive", false);
    }
}
