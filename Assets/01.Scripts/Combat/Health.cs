using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int _currentHealth;

    public Action OnHit;
    public Action OnDied;

    public UnityEvent<Vector2> OnDeathEvent;
    public UnityEvent OnHitEvent;

    private Entity _owner;
    private bool _isDead = false;

    private void Awake()
    {
        _isDead = false;
    }

    public void SetOwner(Entity owner)
    {
        _owner = owner;
        _currentHealth = maxHealth = _owner.Stat.GetMaxHealthValue();
    }

    public void ApplyDamage(int damage, Vector2 attackDirection, Vector2 knockbackPower, Entity dealer)
    {
        if (_isDead) return; //사망하면 더이상 데미지 없음.
        
        //나중에 아머값에 따른 데미지 감소도 해야함
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
    }

    public void ApplyHeal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
        //체력증가에 따른 UI필요.
        Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
    }
}
