using ArmySystem;
using UnityEngine;


public class EnemyMouseEventHandler : MouseEventHandler  
{
    [SerializeField] private Enemy _owner = null;

    public EnemyArmy Target => _owner.MyArmy;

    protected override void OnMouseEnter()
    {
        Target.OnMouseEnter();
    }

    protected override void OnMouseExit()
    {
        Target.OnMouseExit();
    }

    public override void OnClick()
    {
        Target.OnClick();
        ArmyManager.Instance.SetTargetEnemyArmy(Target);
    }

}
