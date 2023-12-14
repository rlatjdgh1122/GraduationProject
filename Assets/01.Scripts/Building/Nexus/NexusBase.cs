using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusBase : MonoBehaviour
{
    #region ������Ʈ
    [SerializeField] private NexusStat _nexusStat;
    private Health _health;
    #endregion

    #region ������Ƽ
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
