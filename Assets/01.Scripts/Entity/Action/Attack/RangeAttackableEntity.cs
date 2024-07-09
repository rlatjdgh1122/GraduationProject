using UnityEngine;

public class RangeAttackableEntity : EntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] protected SingijeonArrow _arrowPrefab;
    [SerializeField] protected Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack(Vector3 targetPos)
    {
        Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as SingijeonArrow;
        arrow.transform.position = _firePos.transform.position;
        arrow.transform.rotation = _firePos.rotation;
        Vector3 dir = new Vector3(_firePos.forward.x, 0, _firePos.forward.z);

        arrow.Setting(owner, DamageCasterCompo.TargetLayer);
        arrow.Fire(dir);
    }
}
