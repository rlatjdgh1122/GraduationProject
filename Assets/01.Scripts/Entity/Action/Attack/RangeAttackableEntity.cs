using UnityEngine;

public class RangeAttackableEntity : EntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack(Vector3 targetPos)
    {
        Vector3 dir = new Vector3(_firePos.forward.x, 0, _firePos.forward.z);
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, Quaternion.LookRotation(dir));

        arrow.Setting(owner, DamageCasterCompo.TargetLayer);
        arrow.Fire(dir);
    }
}
