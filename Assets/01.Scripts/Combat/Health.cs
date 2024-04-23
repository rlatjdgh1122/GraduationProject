using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable, IKnockbackable, IStunable
{
    public int maxHealth;
    public int currentHealth;
    private int _armor;
    private int _evasion;

    public LayerMask groundLayer;

    #region ActionEvent

    public Action OnHit;
    public Action OnDied;

    public UnityEvent WaterFallEvent;
    public UnityEvent OnDeathEvent; //나중에 Vector3인자값
    public UnityEvent OnDashDeathEvent;
    public UnityEvent<float, float> OnUIUpdate;
    public UnityEvent OffUIUpdate;

    private FeedbackController feedbackCompo = null;
    #endregion

    private EntityActionData _actionData;

    private bool _isDead = false;
    public bool IsDead => _isDead;
    public bool IsMaxHP => currentHealth == maxHealth;

    private void Awake()
    {
        _isDead = false;
        _actionData = GetComponent<EntityActionData>();
        feedbackCompo = GetComponent<FeedbackController>();
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

    public bool Stun(RaycastHit ray, float duration)
    {
        GameObject enemy = ray.collider.gameObject;

        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Stun, out var stunF))
        {
            //OnStunEvent?.Invoke();
            stunF.PlayFeedback();

            StartCoroutine(StunCoroutine(enemy, duration));
        }

        return true;
    }

    private IEnumerator StunCoroutine(GameObject enemy, float duration)
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

        NavMeshAgent _navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        float speed = _navMeshAgent.speed;

        if (_navMeshAgent != null)
        {
            _navMeshAgent.speed = 0;
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

        if (_navMeshAgent != null)
        {
            _navMeshAgent.speed = speed;
        }
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType)
    {
        if (_isDead) return;

        float dice = UnityEngine.Random.value;
        float adjustedEvasion = _evasion * 0.01f;
        if (dice < adjustedEvasion)
        {
            if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Evasion, out var evasionF))
            {
                evasionF.PlayFeedback();
            }
            return;
        }

        _actionData.HitPoint = point;
        _actionData.HitNormal = normal;
        _actionData.HitType = hitType;

        float adjustedDamage = damage * (1.0f - (_armor * 0.01f));

        currentHealth = (int)Mathf.Clamp(currentHealth - adjustedDamage, 0, maxHealth);

        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Hit, out var hitF))
        {
            feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Hit);

            OnHit?.Invoke();
            hitF.PlayFeedback();
        }

        OnUIUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }
    public void Stun(float value)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Knockback, out var knockbackF, value))
        {
            knockbackF.PlayFeedback();
        }
    }
    public void Knockback(float value, Vector3 normal = default)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Knockback, out var knockbackF, value))
        {
            //넉백을 통해 바다에 떨어졌다면
            if (!knockbackF.PlayFeedback())
            {
                feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.WaterFall);
            }
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

        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Heal, out var hitF))
        {
            hitF.PlayFeedback();
            //OnHealedEvent?.Invoke();
        }
    }

    private void Dead()
    {
        feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Dead);

        OffUIUpdate?.Invoke();
        _isDead = true;
        OnDied?.Invoke();
    }

   
}
