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
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
        }

        CheckCommandModeForMovement();
        CheckCommandModeForChase();
    }

    public override void Exit()
    {
        base.Exit();

        AttackExit();
    }
}
