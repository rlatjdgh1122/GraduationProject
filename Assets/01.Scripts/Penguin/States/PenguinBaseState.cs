using UnityEngine;

public class PenguinBaseState : PenguinState<BasicPenguinStateEnum> //상속받기 위해서 만든 짜바리 클래스
{
    public PenguinBaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) :
        base(penguin, stateMachine, animBoolName)
    {
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.Input.ClickEvent += HandleClick;
    }

    public override void Enter()
    {
        base.Enter();
    }

    private void HandleClick()
    {
        if (_penguin.IsInside || _penguin.AttackInable) _penguin.IsClickToMoving = true;
        _penguin.SetMovement();
        _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
    }

    public override void Exit()
    {
        base.Exit();
        _penguin.Input.ClickEvent -= HandleClick;
    }
}
