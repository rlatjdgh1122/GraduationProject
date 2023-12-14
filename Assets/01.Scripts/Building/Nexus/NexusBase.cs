using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusBase : MonoBehaviour
{
    #region 컴포넌트
    [SerializeField] private NexusStat _nexusStat;
    private Health _health;
    #endregion

    #region 프로퍼티
    public NexusStat NexusStat => _nexusStat;
    public Health HealthCompo => _health;
    #endregion

    private void Awake()
    {
        _health = GetComponent<Health>();

        _health.SetOwner(_nexusStat);
        _nexusStat.SetOwner(this);


    }
}
