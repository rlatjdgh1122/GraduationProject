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

            if (_penguin.Target.IsDead) //Ÿ���� �׾��ٸ�
            {
                var nearestPlayer = _penguin.FindNearestEnemy("Enemy");
                if (nearestPlayer != null)
                    _penguin.SetTarget(nearestPlayer.transform.position);
                _penguin.StopImmediately(); //������ Ÿ�� ��ġ�� ���� ������ ���� �����̰� ����
            }
            //if (_penguin.Target == null) //Ÿ���� ���ٸ� ������ ����
            //{
            //    _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            //}

            //if (_penguin.Target != null) //Ÿ���� �ִٸ�
            //{
            //    if (!_penguin.IsAttackRange) //Ÿ���� ���ݻ�Ÿ� �ȿ� ���� �ʴٸ�
            //        _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

            //    if (_penguin.Target.IsDead) //Ÿ���� �׾��ٸ�
            //    {
            //        var nearestPlayer = _penguin.FindNearestEnemy("Enemy");
            //        if (nearestPlayer != null)
            //            _penguin.SetTarget(nearestPlayer.transform.position);
            //        _penguin.StopImmediately(); //������ Ÿ�� ��ġ�� ���� ������ ���� �����̰� ����
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
