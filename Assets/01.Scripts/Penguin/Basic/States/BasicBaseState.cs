using UnityEngine;
using UnityEngine.InputSystem;

public class BasicBaseState : PenguinState<BasicPenguinStateEnum> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
{
    public BasicBaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) :
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

        if (_penguin.IsDead)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Dead);
    }

    private void HandleClick()
    {
        if (!_penguin.IsDead)
        {
            if (_penguin.IsInTargetRange || _penguin.IsAttackRange) _penguin.IsClickToMoving = true;
            _penguin.SetClickMovement();
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _penguin.Input.ClickEvent -= HandleClick;
    }
}
