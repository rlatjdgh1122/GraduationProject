using Unity.VisualScripting;
using UnityEngine;

public class BasicBaseState : PenguinState<BasicPenguinStateEnum, Penguin> //상속받기 위해서 만든 짜바리 클래스
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
