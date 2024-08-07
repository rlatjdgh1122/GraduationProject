using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public abstract class BuffBuilding : BaseBuilding
{
    [SerializeField]
    [Range(0.0f, 10.0f)]
    protected float innerDistance;

    [SerializeField] private int maxDectectTarget = 30;

    [SerializeField]
    protected LayerMask _targetLayer;


    [SerializeField] protected StatType buffStatType;

    [SerializeField] protected StatMode buffStatMode;

    [SerializeField]
    private int defaultBuffValue;
    public int DefaultBuffValue => defaultBuffValue;

    [SerializeField]
    private float defaultOutoffRangeBuffDuration;
    public float DefaultOutoffRangeBuffDuration => defaultOutoffRangeBuffDuration;

    private float outoffRangeBuffDuration;
    public float OutoffRangeBuffDuration
    {
        get
        {
            return outoffRangeBuffDuration;
        }
        protected set
        {
            outoffRangeBuffDuration = value;
        }
    }

    protected int buffValue;
    public int BuffValues
    {
        get
        {
            return buffValue;
        }
        protected set
        {
            buffValue = value;
        }
    }

    private Health _health = null;

    public FeedbackPlayer buildingEffectFeedback { get; protected set; } = null;
    [SerializeField] protected List<Collider> _buffTargetList = new();
    [SerializeField] protected List<Collider> _exitTargetList = new();

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += PlayEffect;
        SignalHub.OnBattlePhaseEndEvent += StopEffect;
    }

    public override void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= PlayEffect;
        SignalHub.OnBattlePhaseEndEvent -= StopEffect;
    }

    protected override void Awake()
    {
        base.Awake();
        SetBuffValue(DefaultBuffValue);
        SetOutoffRangeBuffDuration(DefaultOutoffRangeBuffDuration);

        _health = GetComponent<Health>();
        _health.SetHealth(_characterStat);
        _health.enabled = false; // 설치 완료 되기 전까지는 공격 대상 X

        _colls = new Collider[maxDectectTarget];
    }

    private Collider[] _colls;
    private int currentCheckCount = 0;

    protected override void Running()
    {
        if (HealthCompo.IsDead) return;
        if (!WaveManager.Instance.IsBattlePhase) return;

        int count = Physics.OverlapSphereNonAlloc(transform.position, innerDistance, _colls, _targetLayer);

        if (count == currentCheckCount) return;

        if (count > currentCheckCount)
        {
            OnEnter();
        }
        else if (count < currentCheckCount)
        {
            OnExit();
        }

        currentCheckCount = count;
    }


    private void OnEnter()
    {
        CheckEnterTarget();
    }

    private void OnExit()
    {
        CheckExitTarget();
    }

    public void PlayEffect()
    {
        if (!IsInstalled) { return; }

        if (buildingEffectFeedback != null)
            buildingEffectFeedback.PlayFeedback();
    }

    public void StopEffect()
    {
        if (buildingEffectFeedback != null)
            buildingEffectFeedback.FinishFeedback();
    }

    protected void CheckEnterTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, innerDistance, _targetLayer);

        foreach (var coll in colls)
        {
            if (_exitTargetList.Contains(coll)) continue;

            if (!_buffTargetList.Contains(coll))
            {
                _buffTargetList.Add(coll);

                EnterTarget(coll);
            }
        }
    }

    protected void CheckExitTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, innerDistance, _targetLayer);

        foreach (Collider coll in _buffTargetList)
        {
            if (_exitTargetList.Contains(coll)) continue;

            bool found = Array.Exists(colls, x => x.Equals(coll));

            if (found == false)
            {
                AddExitTargetList(coll);
                ExitTarget(coll);
            }

        }//end foreach

        _buffTargetList.RemoveList(_exitTargetList);
    }

    protected void AddExitTargetList(Collider coll)
        => _exitTargetList.Add(coll);

    protected void RemoveExitTargetList(Collider coll)
       => _exitTargetList.Remove(coll);

    protected abstract void EnterTarget(Collider coll);
    protected abstract void ExitTarget(Collider coll);

    private void OnMouseEnter()
    {
        if (IsInstalled)
        {
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }

    private void OnMouseExit()
    {
        if (IsInstalled)
        {
            _health.OffUIUpdate?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, innerDistance);
    }

    protected abstract void SetBuffValue(int value);
    protected abstract int GetBuffValue();
    protected abstract void SetOutoffRangeBuffDuration(float value);
    protected abstract float GetOutoffRangeBuffDuration();

    protected override void SetInstalled()
    {
        base.SetInstalled();

        _health.enabled = true; // 설치 완료 되면 공격 대상 O
    }

    public void Clear()
    {
        _buffTargetList.TryClear();
        _exitTargetList.TryClear();
    }
}
