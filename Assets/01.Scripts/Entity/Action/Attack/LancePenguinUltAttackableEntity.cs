using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePenguinUltAttackableEntity : EntityAttackData
{
    [Header("MagicAttack Info")]
    [SerializeField] private LanceUltTruck _truck;
    [SerializeField] private Transform _truckPos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TruckAttack(Vector3 mousePos)
    {
        _truckPos.LookAt(mousePos);

        LanceUltTruck truck = Instantiate(_truck, _truckPos.transform.position, _truckPos.transform.rotation);

        truck.Setting(owner, DamageCasterCompo.TargetLayer);
        truck.TruckMove();
    }
}
