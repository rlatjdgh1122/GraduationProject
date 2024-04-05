using Unity.VisualScripting;
using UnityEngine;

public class BasicBaseState : PenguinState<BasicPenguinStateEnum, Penguin> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
{
    public BasicBaseState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
