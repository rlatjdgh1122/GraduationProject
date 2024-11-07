using ArmySystem;
using UnityEngine;


public class EnemyMouseEventHandler : MouseEventHandler
{
    [SerializeField] private Enemy _owner = null;

    public EnemyArmy Target => _owner?.MyArmy;

    protected override void OnMouseEnter()
    {
        Target.SetSingleTarget(_owner);
        Target.OnMouseEnter();
    }

    protected override void OnMouseExit()
    {
        Target.SetSingleTarget(null);
        Target.OnMouseExit();
    }

    public override void OnClick()
    {
        Debug.Log(Target);
        Target.OnClick();
        ArmyManager.Instance.SetTargetEnemyArmy(Target);
    }

}
