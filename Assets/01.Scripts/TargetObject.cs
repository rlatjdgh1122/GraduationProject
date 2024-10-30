using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(FeedbackController))]
public abstract class TargetObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;

    public BaseStat Stat
    {
        get => _characterStat;
        set => _characterStat = value;
    }

    //사실 as랑 같음
    public T ReturnGenericStat<T>() where T : BaseStat => (T)_characterStat;

    public TargetObject CurrentTarget;

    public bool IsDead
    {
        get
        {
            if (HealthCompo != null)
                return HealthCompo.IsDead;
            return false;
        }
        set
        {
            HealthCompo.IsDead = value;
        }
    }
    public Health HealthCompo { get; private set; }

    private IDeadable _deadCompo = null;

    private Transform nexusTrm = null;
    public Collider ColliderCompo { get; set; } = null;

    [SerializeField] private int _maxDetectEnemy = 5;
    private Collider[] _targetColliders;

    private float radius = 0f;

    protected virtual void Awake()
    {
        _targetColliders = new Collider[_maxDetectEnemy];

        HealthCompo = transform?.GetComponent<Health>();
        ColliderCompo = GetComponent<Collider>();
        _deadCompo = transform?.GetComponent<IDeadable>();

        HealthCompo?.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);

        if (HealthCompo != null)
        {
            HealthCompo.OnHit += HandleHit;
            HealthCompo.OnDied += HandleDie;
        }

        nexusTrm = GameManager.Instance.NexusTrm;

        //내 위치와 transform.forward * 5에서 가장 가까운 포인트의 거리를 비교 => 내 크기의 반지름(원형모형 기준)
        if (ColliderCompo != null)
            radius = Vector3.Magnitude(transform.position - ColliderCompo.ClosestPoint(transform.right * 20));
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }

    public virtual Vector3 GetClosetPostion(Vector3 targetPos)
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 result = (dir * radius) + transform.position;
        result.y = transform.position.y;

        return result;
    }

    public void SetTarget(TargetObject target)
    {
        CurrentTarget = target;
    }

    public T FindNearestTarget<T>(float checkRange, LayerMask mask) where T : TargetObject
    {
        T target = null;

        float maxDistance = 300f;

        int count = Physics.OverlapSphereNonAlloc(transform.position, checkRange, _targetColliders, mask);

        for (int i = 0; i < count; ++i)
        {
            Collider collider = _targetColliders[i];
            if (collider.TryGetComponent(out T potentialTarget))
            {
                float distanceToTarget = Vector3.Distance(transform.position, potentialTarget.transform.position);

                if (distanceToTarget < maxDistance)
                {
                    target = potentialTarget;
                    maxDistance = distanceToTarget;
                }
            }
        }
        return target;
    }

    #region Stat

    public void AddStat(Ability incStat)
    {
        AddStat(incStat.value, incStat.statType, incStat.statMode);
    }

    public void RemoveStat(Ability incStat)
    {
        RemoveStat(incStat.value, incStat.statType, incStat.statMode);
    }

    public void AddStat(List<Ability> abilities)
    {
        foreach (var incStat in abilities)
        {
            AddStat(incStat.value, incStat.statType, incStat.statMode);
        }
    }

    public void RemoveStat(List<Ability> abilities)
    {
        foreach (var incStat in abilities)
        {
            RemoveStat(incStat.value, incStat.statType, incStat.statMode);
        }
    }

    public void AddStat(int value, StatType type, StatMode mode)
    {
        Stat.AddStat(value, type, mode);
    }

    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        Stat.RemoveStat(value, type, mode);
    }

    #endregion


    protected abstract void HandleHit();

    protected virtual void HandleDie()
    {
        _deadCompo?.OnDied();
    }

    public override void Init()
    {
        _deadCompo?.OnResurrected();
    }

    public virtual void OnResurrected()
    {
        _deadCompo?.OnResurrected();
    }

    protected virtual void OnDestroy()
    {
        if (HealthCompo != null)
        {
            HealthCompo.OnHit -= HandleHit;
            HealthCompo.OnDied -= HandleDie;
        }

    }
}
