using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;

    public ResourceType resourceType;

    public Health HealthCompo { get; private set; }
    public EntityActionData ActionData { get; private set; }

    protected virtual void Awake()
    {
        HealthCompo = GetComponent<Health>();
        ActionData = GetComponent<EntityActionData>();

        HealthCompo.SetHealth(_characterStat);
        _characterStat = Instantiate(_characterStat);

        HealthCompo.OnDied += HandleDie;
    }

    protected virtual void HandleDie()
    {

    }
}
