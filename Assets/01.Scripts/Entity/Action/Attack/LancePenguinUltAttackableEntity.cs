using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePenguinUltAttackableEntity : EntityAttackData
{
    [Header("MagicAttack Info")]
    [SerializeField] private LanceUltTruck _lanceUltTruck;
    [SerializeField] private Transform _spawnPos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TruckAttack(Vector3 mousePos)
    {
        _spawnPos.LookAt(new Vector3(0, mousePos.y, 0));

        LanceUltTruck truck = PoolManager.Instance.Pop(_lanceUltTruck.name) as LanceUltTruck;
        
        truck.Setting(owner, DamageCasterCompo.TargetLayer);
        truck.TruckMove(mousePos);
    }
}
