using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public abstract class TargetObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;
    public BaseStat Stat => _characterStat;

    //사실 as랑 같음
    public T ReturnGenericStat<T>() where T : BaseStat => (T)_characterStat;

    public TargetObject CurrentTarget;

    public bool IsDead = false;
    public Health HealthCompo { get; private set; }

    private Transform nexusTrm = null;
    protected Collider _collider = null;

    [SerializeField] private int _maxDetectEnemy = 5;
    private Collider[] _targetColliders;

    protected virtual void Awake()
    {
        _targetColliders = new Collider[_maxDetectEnemy];

        HealthCompo = transform?.GetComponent<Health>();
        _collider = GetComponent<Collider>();

        HealthCompo?.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);
        if (HealthCompo != null)
        {
            HealthCompo.OnHit += HandleHit;
            HealthCompo.OnDied += HandleDie;
        }

        nexusTrm = GameManager.Instance.NexusTrm;
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {
    }

    public virtual Vector3 GetClosetPostion(Transform targetTrm)
    {
        //내 위치와 Vector3.zero에서 가장 가까운 포인트의 거리를 비교 => 내 크기의 반지름(원형모형 기준)
        float radius = Vector3.Distance(transform.position, _collider.ClosestPoint(Vector3.zero));
        Vector3 dir = (targetTrm.position - transform.position).normalized;
        Vector3 result = dir * radius + transform.position;
        result.y = transform.position.y;

        return result;
    }

    public void SetTarget(TargetObject target)
    {
        CurrentTarget = target;
    }

    public T FindNearestTarget<T>(LayerMask mask) where T : TargetObject
    {
        T target = null;
        float radius = 10f;
        float maxDistance = 300f;

        // 넥서스 기준으로 주변 객체 검색
        int count = Physics.OverlapSphereNonAlloc(nexusTrm.position, radius, _targetColliders, mask);
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
    protected abstract void HandleDie();

    protected virtual void OnDestroy()
    {
        if (HealthCompo != null)
        {
            HealthCompo.OnHit -= HandleHit;
            HealthCompo.OnDied -= HandleDie;
        }
    }
}
