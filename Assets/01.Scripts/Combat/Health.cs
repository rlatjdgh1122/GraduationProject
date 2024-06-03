using StatOperator;
using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FeedbackController))]
public class Health : MonoBehaviour, IDamageable, IKnockbackable, IStunable, IProvokedable
{
    public int maxHealth;
    public int currentHealth;

    private int armor
    {
        get
        {
            var result = _onwer.armor.GetValue();
            return result;
        }
    }

    private float evasion
    {
        get
        {
            var result = _onwer.evasion.GetValue() * 0.01f;
            return result;
        }
    }

    public LayerMask groundLayer;

    private BaseStat _onwer;

    #region ActionEvent

    public Action OnHit;
    public Action OnDied;

    public UnityEvent WaterFallEvent;
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnDashDeathEvent;
    public UnityEvent<float, float> OnUIUpdate;
    public UnityEvent OffUIUpdate;


    private FeedbackController feedbackCompo = null;
    #endregion

    private EntityActionData _actionData;

    public bool IsDead = false;
    public bool OnHpBarUI = false;
    public bool IsAlwaysShowUI { get; set; } = false;

    public bool IsMaxHP => currentHealth == maxHealth;

    private void Awake()
    {
        IsDead = false;
        _actionData = GetComponent<EntityActionData>();
        feedbackCompo = GetComponent<FeedbackController>();
    }

    public void SetHealth(BaseStat owner)
    {
        _onwer = owner;
        currentHealth = maxHealth = owner.GetMaxHealthValue();
    }

    public void SetMaxHealth(BaseStat owner)
    {
        maxHealth = owner.GetMaxHealthValue();
    }

    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType, TargetObject hitTarget, bool isFeedback = true)
    {
        if (IsDead) return;

        float dice = UnityEngine.Random.value;
        if (dice < evasion)
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

        int finalDamage = StatCalculator.GetDamage(damage, armor);
        currentHealth = Mathf.Clamp(currentHealth - finalDamage, 0, maxHealth);

        /*if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Hit, out var hitF))
        {
            feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Hit);

            OnHit?.Invoke();

            if (isFeedback) { hitF.PlayFeedback(); }
        }*/

        //HitType에 따라 맞는 소리를 다르게
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Hit, out var hitF))
        {
            switch (hitType)
            {
                case HitType.IceHit:
                    feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.MeleeHit);
                    break;
                case HitType.MopHit:
                    feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.MopHit);
                    break;
                case HitType.ArrowHit:
                    feedbackCompo.TryPlayHitSoundFeedback(HitSoundFeedbackEnumType.ArrowHit);
                    break;
                default:
                    //일단 비워
                    break;
            }

            OnHitEvent?.Invoke();
            OnHit?.Invoke();

            if (isFeedback)
            {
                hitF.PlayFeedback();
            }

        }//end if


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
            if (value <= 0) return;

            stunF.PlayFeedback();
        }
    }

    public void Knockback(float value, Vector3 normal = default)
    {
        if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Knockback, out var knockbackF, value))
        {
            if (value <= 0) return;

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
            if (value <= 0) return;

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

        OnUIUpdate?.Invoke(currentHealth, maxHealth);
        //나중에 바꾸기
        //if (feedbackCompo.TryGetFeedback(FeedbackEnumType.Heal, out var hitF))
        //{
        //    hitF.PlayFeedback();
        //    OnHealedEvent?.Invoke();
        //}
    }

    private void Dead()
    {
        feedbackCompo.TryPlaySoundFeedback(SoundFeedbackEnumType.Dead);
        OnDied?.Invoke();
        OnDeathEvent?.Invoke();
        OffUIUpdate?.Invoke();
        IsDead = true;
    }


}
