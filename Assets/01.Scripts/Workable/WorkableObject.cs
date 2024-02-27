using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkableObject : PoolableMono
{
    [SerializeField] protected BaseStat _characterStat;

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
