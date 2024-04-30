using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
public abstract class TargetObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;
    public BaseStat Stat => _characterStat;
    public T ReturnGenericStat<T>() where T : BaseStat => (T)_characterStat;

    public TargetObject CurrentTarget;

    public bool IsDead = false;
    public Health HealthCompo { get; private set; }
    public CharacterController CharacterCompo { get; private set; }

    [SerializeField] private int _maxDetectEnemy = 5;
    private Transform nexusTrm = null;

    private Collider[] _colliders;
    protected virtual void Awake()
    {
        _colliders = new Collider[_maxDetectEnemy];

        HealthCompo = transform?.GetComponent<Health>();
        CharacterCompo = GetComponent<CharacterController>();

        HealthCompo?.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);

        if (HealthCompo != null)
        {
            HealthCompo.OnHit += HandleHit;
            HealthCompo.OnDied += HandleDie;
        }

        nexusTrm ??= GameManager.Instance.NexusTrm;
    }
    protected virtual void Start()
    {
    }
    protected virtual void Update()
    {

    }


    public void SetTarget(TargetObject target)
    {
        CurrentTarget = target;
    }

    public TargetObject FindNearestTarget(LayerMask mask)
    {
        TargetObject target = null;
        float radius = 10f;
        float maxDistance = 300f;

        // 넥서스 기준으로 주변 객체 검색
        int count = Physics.OverlapSphereNonAlloc(nexusTrm.position, radius, _colliders, mask);
        for (int i = 0; i < count; ++i)
        {
            Collider collider = _colliders[i];

            if (collider.TryGetComponent(out TargetObject potentialTarget))
            {
                float distanceToTarget = Vector3.Distance(potentialTarget.transform.position, nexusTrm.position);

                if (distanceToTarget < maxDistance)
                {
                    target = potentialTarget;
                    maxDistance = distanceToTarget;
                }
            }
        }

        return target;
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
