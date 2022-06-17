using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _health;
    [SerializeField] private Transform _shootPoint;
    


    private Weapon _currentWeapon;
    private int _currentWeaponIndex;
    private int _currentHealth;
    private Animator _animator;
    
    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
       
        ChangeWeapon(_weapons[_currentWeaponIndex]);
        _currentHealth = _health;
        _animator = GetComponent<Animator>();
        HealthChanged(_currentHealth, _health);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        HealthChanged(_currentHealth, _health);
        if (_currentHealth <= 0)
            Die();

    }

  

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ) 
        {
            if (EventSystem.current.currentSelectedGameObject != null)
                if (EventSystem.current.currentSelectedGameObject.layer == 5)
                    return;
            _currentWeapon.Shoot(_shootPoint);
        }
    }

    public void OnEnemyDied(int reward) {
        Money += reward;
    }

    public void Die() {
        Destroy(gameObject);
    }
    public void AddMoney(int money) {
        Money += money;
        MoneyChanged?.Invoke(Money);
    }
    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        _weapons.Add(weapon);
        MoneyChanged?.Invoke(Money);
    }
    public void NextWeapon()
    {

        if (++_currentWeaponIndex == _weapons.Count)
            _currentWeaponIndex = 0;
        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }
    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex == -1)
            _currentWeaponIndex = _weapons.Count-1;
        ChangeWeapon(_weapons[_currentWeaponIndex]);
    }
    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
    } 
}
