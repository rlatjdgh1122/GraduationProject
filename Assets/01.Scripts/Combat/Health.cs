using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    #region ActionEvent
    public Action OnHit;
    public Action OnDied;
    public UnityEvent OnHealedEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent; //���߿� Vector3���ڰ�
    public UnityEvent<float, float> OnUIUpdate;
    public UnityEvent OffUIUpdate;
    #endregion

    private EntityActionData _actionData;

    private bool _isDead = false;
    public bool IsMaxHP => currentHealth == maxHealth;

    private void Awake()
    {
        _isDead = false;
        _actionData = GetComponent<EntityActionData>();
    }

    public void SetHealth(BaseStat owner)
    {
        Instantiate(owner);
        currentHealth = maxHealth = owner.GetMaxHealthValue();
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType)
    {
        if (_isDead) return;

        _actionData.HitPoint = point;
        _actionData.HitNormal = normal;
        _actionData.HitType = hitType;

        OnHitEvent?.Invoke();
        OnHit?.Invoke();

        //���߿� �ƸӰ��� ���� ������ ���ҵ� �ؾ���
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnUIUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            _isDead = true;
            OnDeathEvent?.Invoke();
            OnDied?.Invoke();
        }
    }

    public void ApplyHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnUIUpdate?.Invoke(currentHealth, maxHealth);
        OnHealedEvent?.Invoke();
    }
}
