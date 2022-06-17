using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDalay;

    private float _lastTimeAttack;
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (_lastTimeAttack <= 0) {
            Attack(Target);
            _lastTimeAttack = _attackDalay;
        }
        _lastTimeAttack -= Time.deltaTime;
    }

    private void Attack(Player target) {
        _animator.Play("Attack");
        target.ApplyDamage(_damage);
    }
}
