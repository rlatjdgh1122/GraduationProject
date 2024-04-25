using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(CharacterController))]
public abstract class Target : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;
    public BaseStat Stat => _characterStat;

    public Target CurrentTarget;

    public bool IsDead = false;
    public Health HealthCompo { get; private set; }
    public CharacterController CharacterCompo { get; private set; }

    [SerializeField] private int _maxDetectEnemy = 5;
    private Collider[] _colliders;
    private Transform nexusTrm = GameManager.Instance.NexusTrm;
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
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    public T FindNearestTarget<T>(LayerMask mask) where T : Target //OnProvoked bool�� ����
    {
        T target = null;
        float radius = 10f;
        float distance = 300f;

        //넥서스 기준으로 찾음
        int count = Physics.OverlapSphereNonAlloc(nexusTrm.position, radius, _colliders, mask);

        for (int i = 0; i < count; ++i)
        {
            var targetObj = _colliders[i].gameObject;
            if (targetObj is not T) continue;

            if (distance > Vector3.Distance(targetObj.transform.position, nexusTrm.position))
            {
                target = targetObj as T;
            }
        }
        if (target is not null)
            return target;

        return default;
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
