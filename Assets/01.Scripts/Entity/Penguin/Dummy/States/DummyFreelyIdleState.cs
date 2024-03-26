using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyFreelyIdleState : DummyBaseState
{
    private float timer = 0f;
    private float randomTime = 5f;

    public DummyFreelyIdleState(Penguin penguin, EntityStateMachine<DummyPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        timer = 0f;
        randomTime = Random.Range(1.0f, 5.0f);

        _penguin.StopImmediately();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //일정 시간 지나면 랜덤으로 상태변환
        timer += Time.deltaTime;
        if (timer > randomTime)
        {
            _stateMachine.ChangeState(RandomState());
        }


    }
    public override void Exit()
    {
        base.Exit();
    }


}
