using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancePenguinUltAttackableEntity : EntityAttackData
{
    [Header("TruckAttack Info")]
    [SerializeField] private LanceUltTruck _truck;
    [SerializeField] private Transform _truckPos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TruckAttack(Vector3 mousePos)
    {
        Vector3 direction = mousePos - _truckPos.position;
        direction.y = 0; // 수평 방향으로만 회전하도록 Y축을 0으로 설정

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _truckPos.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }

        LanceUltTruck truck = Instantiate(_truck, _truckPos.transform.position, _truckPos.transform.rotation);

        truck.Setting(owner, DamageCasterCompo.TargetLayer);
        truck.TruckMove();
    }
}
