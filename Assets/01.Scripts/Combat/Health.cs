using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;
    private int _armor;
    private int _evasion;

    public LayerMask groundLayer;

    #region ActionEvent

    public Action OnHit;
    public Action OnDied;
    //OnDied가 실행된다음 실행
    public Action OnDiedEndEvent;

    public UnityEvent OnHealedEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnStunEvent;
    public UnityEvent OnEvasionEvent;
    public UnityEvent WaterFallEvent;
    public UnityEvent OnDeathEvent; //나중에 Vector3인자값
    public UnityEvent OnDashDeathEvent;
    public UnityEvent<float, float> OnUIUpdate;
    public UnityEvent OffUIUpdate;
    #endregion

    private EntityActionData _actionData;

    private bool _isDead = false;
    public bool IsDead => _isDead;
    public bool IsMaxHP => currentHealth == maxHealth;

    private void Awake()
    {
        _isDead = false;
        _actionData = GetComponent<EntityActionData>();
    }

    public void SetHealth(BaseStat owner)
    {
        currentHealth = maxHealth = owner.GetMaxHealthValue();
        _armor = owner.armor.GetValue();
        _evasion = owner.evasion.GetValue();
    }

    public void SetMaxHealth(BaseStat owner)
    {
        maxHealth = owner.GetMaxHealthValue();
    }

    public bool KnockBack(float value = 1, Vector3 normal = default)
    {
        Vector3 currentPosition = transform.position;

        Vector3 knockbackPosition = currentPosition - new Vector3(normal.x, 0f, normal.z) * value;

        transform.DOMove(knockbackPosition, 0.5f);

        if (!IsPositionValid(knockbackPosition))
        {
            WaterFallEvent?.Invoke();

            transform.DOMoveY(transform.position.y - 2f, 1.2f);

            Dead();
            return false;
        }
        else
            return true;
    }

    bool IsPositionValid(Vector3 position)
    {
        return Physics.Raycast(position, new Vector3(0, -1, 0), 5f, groundLayer);
    }

    public bool Stun(RaycastHit ray, float duration)
    {
        GameObject enemy = ray.collider.gameObject;
        //Debug.Log(enemy.name + "이(가) 스턴 상태가 되었습니다.");
        OnStunEvent?.Invoke();

        StartCoroutine(StunCoroutine(enemy, duration));
        
        //Debug.Log(enemy.name + "이(가) 스턴 상태에서 벗어났습니다.");
        return true;
    }

    private IEnumerator StunCoroutine(GameObject enemy ,float duration)
    {
        Animator animator = enemy.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.speed = 0f;
        }

        CharacterController controller = enemy.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; 
        }

        yield return new WaitForSeconds(duration);

        if (animator != null)
        {
            animator.speed = 1f; 
        }

        if (controller != null)
        {
            controller.enabled = true; 
        }
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType, bool isPlayHitEvent = true)
    {
        if (_isDead) return;

        float dice = UnityEngine.Random.value;
        float adjustedEvasion = _evasion * 0.01f;
        if (dice < adjustedEvasion)
        {
            OnEvasionEvent?.Invoke();
            return;
        }

        _actionData.HitPoint = point;
        _actionData.HitNormal = normal;
        _actionData.HitType = hitType;

        float adjustedDamage = damage * (1.0f - (_armor * 0.01f));

        currentHealth = (int)Mathf.Clamp(currentHealth - adjustedDamage, 0, maxHealth);

        if (isPlayHitEvent) { OnHitEvent?.Invoke(); }
        OnHit?.Invoke();
        OnUIUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void ApplyHitType(HitType hitType)
    {
        _actionData.HitType = hitType;
    }

    public void ApplyHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        OnUIUpdate?.Invoke(currentHealth, maxHealth);
        OnHealedEvent?.Invoke();
    }

    private void Dead()
    {
        OffUIUpdate?.Invoke();
        _isDead = true;
        OnDeathEvent?.Invoke();
        OnDied?.Invoke();
        OnDiedEndEvent?.Invoke();
    }
}
