using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    public Action OnHit;
    public Action OnDied;

    public UnityEvent<Vector2> OnDeathEvent;
    public UnityEvent<int> OnUIUpdate;

    private Entity _owner;
    private bool _isDead = false;

    private void Awake()
    {
        _isDead = false;
    }

    public void SetOwner(BaseStat owner)
    {
        currentHealth = maxHealth = owner.GetMaxHealthValue();
    }

    public void ApplyDamage(int damage, Vector2 attackDirection, Vector2 knockbackPower, Entity dealer)
    {
        if (_isDead) return; //사망하면 더이상 데미지 없음.

        OnHit?.Invoke();
        OnUIUpdate?.Invoke(damage);
        //나중에 아머값에 따른 데미지 감소도 해야함
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0)
        {
            _isDead = true;
            OnDied?.Invoke();
        }
    }

    public void ApplyHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        //체력증가에 따른 UI필요.
        Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
    }
}
