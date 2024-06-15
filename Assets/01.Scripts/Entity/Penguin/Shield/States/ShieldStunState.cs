using ArmySystem;
using System.Linq;
using Unity.VisualScripting;

public class ShieldStunState : ShieldBaseState
{

    public ShieldStunState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //�Ѹ��� �����ٰ� ������ 
    {
        base.Enter();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled) //����
        {
            IsTargetNull(ShieldPenguinStateEnum.Idle);
        }

        CheckCommandModeForMovement();
        CheckCommandModeForChase();
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
