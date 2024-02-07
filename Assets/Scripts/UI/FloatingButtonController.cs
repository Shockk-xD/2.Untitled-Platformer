using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloatingButtonController : MonoBehaviour {
    [SerializeField] private GameObject _floatingButton;
    [SerializeField, TextArea] private string _label;
    private Text _floatingButtonText;
    private Animator _floatingButtonAnimator;

    public UnityAction onClick;
    public Action triggerExitAction;
    [HideInInspector] public bool isErrorPresent = false;

    private void Start() {
        _floatingButtonAnimator = _floatingButton.GetComponent<Animator>();
        _floatingButtonText = _floatingButton.GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision == null || !collision.CompareTag("Player")) return;

        _floatingButtonText.text = _label;
        if (onClick != null)
            _floatingButton.GetComponent<Button>().onClick.AddListener(onClick);
        _floatingButton.GetComponent<Button>().onClick.AddListener(() => {
            if (isErrorPresent)
                _floatingButtonAnimator.SetTrigger("Error");
            _floatingButtonAnimator.SetBool("FButtonIsOpen", false);
        });
        _floatingButtonAnimator.SetBool("FButtonIsOpen", true);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision == null || !collision.CompareTag("Player")) return;
        triggerExitAction?.Invoke();
        _floatingButton.GetComponent<Button>().onClick.RemoveListener(onClick);
        _floatingButtonAnimator.SetBool("FButtonIsOpen", false);
    }
}
