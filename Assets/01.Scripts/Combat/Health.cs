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

    public UnityEvent OnDeathEvent; //나중에 Vector3인자값
    public UnityEvent<float, float> OnUIUpdate;
    public UnityEvent OffUIUpdate;

    private Entity _owner;
    private bool _isDead = false;
    public bool IsMaxHP => currentHealth == maxHealth;
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
        if (_isDead)
        {
            OnDeathEvent?.Invoke();
            return;
        }

        OnHit?.Invoke();
        //나중에 아머값에 따른 데미지 감소도 해야함
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnUIUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            _isDead = true;
            OnDied?.Invoke();
        }
    }

    public void ApplyHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnUIUpdate?.Invoke(currentHealth, maxHealth);
        //Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
    }
}
