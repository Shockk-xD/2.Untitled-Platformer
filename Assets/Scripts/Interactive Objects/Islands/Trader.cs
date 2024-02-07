using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    public static bool isActive = false;

    [SerializeField] private GameObject[] _prices;
    [SerializeField] private GameObject[] _itemsToSell;

    [SerializeField] private RectTransform _priceHolder;
    [SerializeField] private RectTransform _itemHolder;

    [SerializeField] private GameObject _speedStaffItem;

    [SerializeField] private Animator _buyButtonAnimator;
    [SerializeField] private Animator _tradeShop;

    private int _currentItemIndex = 0;

    private GameObject _price;
    private GameObject _item;

    private Player _player;

    private void Start() {
        _player = Player.GetPlayer();
        SwipeDetector.SwipeEvent += ShowNextItem;
        if (_prices.Length > 0 && _itemsToSell.Length > 0) {
            _price = Instantiate(_prices[0], _priceHolder);
            _item = Instantiate(_itemsToSell[0], _itemHolder);
        }
    }

    private void ShowNextItem(bool isRight) {
        if (!isActive) return;

        if (isRight) {
            _currentItemIndex++;
            if (_currentItemIndex > _prices.Length - 1)
                _currentItemIndex = 0;
        } else {
            _currentItemIndex--;
            if (_currentItemIndex < 0)
                _currentItemIndex = _prices.Length - 1;
        }

        Destroy(_price);
        Destroy(_item);

        _price = Instantiate(_prices[_currentItemIndex], _priceHolder);
        _item = Instantiate(_itemsToSell[_currentItemIndex], _itemHolder);
    }

    public void OnBuyButtonClick() {
        if (_currentItemIndex == 0) {
            if (_player.TryRemoveResource(ResourceInventory.Resource.Wood, 1)) {
                _player.TryAddItem(_speedStaffItem);
                CloseShop();
            } else
                StartCoroutine(ButtonTextAnimator());

            return;
        }

        StartCoroutine(ButtonTextAnimator());
    }

    public IEnumerator ButtonTextAnimator() {
        _buyButtonAnimator.GetComponentInChildren<Text>().text = "Не хватает ресурсов!";
        _buyButtonAnimator.SetTrigger("Cancel");
        yield return new WaitForSeconds(0.85f);
        _buyButtonAnimator.GetComponentInChildren<Text>().text = "Купить";
    }

    public void CloseShop() {
        _tradeShop.SetBool("IsOpen", false);
    }
}
