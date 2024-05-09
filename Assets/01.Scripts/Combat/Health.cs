using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FeedbackController))]
public class Health : MonoBehaviour, IDamageable, IKnockbackable, IStunable, IProvokedable
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
    public UnityEvent OnDeathEvent;
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

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType, TargetObject hitTarget, bool isFeedback = true)
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
        _actionData.HitTarget = hitTarget;

        float adjustedDamage = damage * (1.0f - (_armor * 0.01f));

        currentHealth = (int)Mathf.Clamp(currentHealth - adjustedDamage, 0, maxHealth);

        /*if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Hit, out var hitF))
        {
            feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Hit);

            OnHit?.Invoke();

            if (isFeedback) { hitF.PlayFeedback(); }
        }*/

        //HitType에 따라 맞는 소리를 다르게
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Hit, out var hitF))
        {
            if(HitType.IceHit == hitType)
            {
                feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.MeleeHit);
            }
            else if(HitType.MopHit == hitType) 
            {
                feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.MopHit);
            }
            else if (HitType.ArrowHit == hitType)
            {
                feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.ArrowHit);
            }

            OnHit?.Invoke();

            if (isFeedback) { hitF.PlayFeedback(); }
        }

        OnUIUpdate?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Stun(float value)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Stun, out var stunF, value))
        {
            stunF.PlayFeedback();
        }
    }

    public void Knockback(float value, Vector3 normal = default)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Knockback, out var knockbackF, value))
        {
            knockbackF.PlayFeedback();

            //바다에 빠졌다면
            if (!knockbackF.IsSuccessed)
            {
                feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.WaterFall);
                Dead();
            }
        }
    }

    public void Provoked(float value)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Provoked, out var ProvokedF, value))
        {
            ProvokedF.PlayFeedback();
        }
    }
    public void ApplyHitType(HitType hitType)
    {
        _actionData.HitType = hitType;
    }

    public void ApplyHeal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        //나중에 바꾸기
        //OnUIUpdate?.Invoke(currentHealth, maxHealth);
        //if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Heal, out var hitF))
        //{
        //    hitF.PlayFeedback();
        //    OnHealedEvent?.Invoke();
        //}
    }

    private void Dead()
    {
        feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Dead);
        OnDeathEvent?.Invoke();
        OffUIUpdate?.Invoke();
        _isDead = true;
        OnDied?.Invoke();
    }


}
