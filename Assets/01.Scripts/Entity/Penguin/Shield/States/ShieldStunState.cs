using ArmySystem;
using System.Linq;
using Unity.VisualScripting;

public class ShieldStunState : ShieldBaseState
{

    public ShieldStunState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled) //공격
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
