using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;
    [SerializeField] private float _noiseValue;

    public ResourceType resourceType;
    public float NoiseValue => _noiseValue;

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
    private void OnDestroy()
    {
        if (HealthCompo != null)
            HealthCompo.OnDied -= HandleDie;
    }
    protected virtual void HandleDie()
    {

    }
}
