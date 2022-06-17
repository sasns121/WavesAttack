using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    [SerializeField] private WeaponCardView _weaponCardTamplate;
    [SerializeField] private GameObject _contantContainer;


    private void Start()
    {
        for(int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_weaponCardTamplate, _contantContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(weapon);
    }
    private void OnSellButtonClick(Weapon weapon, WeaponCardView card)
    {
        TrySellWeapon(weapon, card);
    }
    private void TrySellWeapon(Weapon weapon, WeaponCardView card)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            weapon.Buy();
            card.SellButtonClick -= OnSellButtonClick;
        }
    }
}
