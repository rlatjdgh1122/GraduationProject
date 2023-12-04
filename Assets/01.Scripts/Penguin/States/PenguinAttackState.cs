using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttackState : PenguinBaseState
{
    public PenguinAttackState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            if (!_penguin.IsAttackRange)
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

            if (_penguin.Target.IsDead) //타겟이 죽었다면
            {
                var nearestPlayer = _penguin.FindNearestEnemy("Enemy");
                if (nearestPlayer != null)
                    _penguin.SetTarget(nearestPlayer.transform.position);
                _penguin.StopImmediately(); //무조건 타겟 위치로 가기 때문에 가는 딜레이가 생김
            }
            //if (_penguin.Target == null) //타겟이 없다면 가만히 있음
            //{
            //    _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            //}

            //if (_penguin.Target != null) //타겟이 있다면
            //{
            //    if (!_penguin.IsAttackRange) //타겟이 공격사거리 안에 있지 않다면
            //        _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

            //    if (_penguin.Target.IsDead) //타겟이 죽었다면
            //    {
            //        var nearestPlayer = _penguin.FindNearestEnemy("Enemy");
            //        if (nearestPlayer != null)
            //            _penguin.SetTarget(nearestPlayer.transform.position);
            //        _penguin.StopImmediately(); //무조건 타겟 위치로 가기 때문에 가는 딜레이가 생김
            //    }
            //} 
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
