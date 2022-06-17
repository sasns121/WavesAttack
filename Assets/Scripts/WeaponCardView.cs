using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class WeaponCardView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Button _buyButton;

    private Weapon _weapon;

    public event UnityAction<Weapon, WeaponCardView> SellButtonClick;

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnButtonClick);
        _buyButton.onClick.AddListener(TryLockItem);
    }
    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnButtonClick);
        _buyButton.onClick.RemoveListener(TryLockItem);
    }
    public void Render(Weapon weapon)
    {
        _weapon = weapon;
        _label.text = weapon.Label.ToString();
        _price.text = weapon.Price.ToString();
        _icon.sprite = weapon.Icon;
    }

    private void TryLockItem()
    {
        if (_weapon.IsBuyed)
            _buyButton.interactable = false;
    }


    private void OnButtonClick() {
        SellButtonClick?.Invoke(_weapon, this);
    }
}
