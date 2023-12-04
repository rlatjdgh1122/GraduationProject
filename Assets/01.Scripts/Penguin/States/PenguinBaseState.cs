using UnityEngine;

public class PenguinBaseState : PenguinState<BasicPenguinStateEnum> //상속받기 위해서 만든 짜바리 클래스
{
    public PenguinBaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) :
        base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.Input.ClickEvent += HandleClick;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    private void HandleClick()
    {
        if (_penguin.IsInside || _penguin.IsAttackRange) _penguin.IsClickToMoving = true;
        _penguin.SetClickMovement();
        _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
        _penguin.Input.ClickEvent -= HandleClick;
    }
}
