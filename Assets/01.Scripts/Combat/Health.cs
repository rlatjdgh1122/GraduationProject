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
        if (_isDead) return; //����ϸ� ���̻� ������ ����.

        OnHit?.Invoke();
        OnUIUpdate?.Invoke(damage);
        //���߿� �ƸӰ��� ���� ������ ���ҵ� �ؾ���
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
        //ü�������� ���� UI�ʿ�.
        Debug.Log($"{_owner.gameObject.name} is healed!! : {amount}");
    }
}
