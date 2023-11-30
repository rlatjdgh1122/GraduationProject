using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PenguinState
{
    protected PenguinStateMachine _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����

    protected bool IsInside => Vector3.Distance(_penguin.transform.position, _penguin.enemy.position) <= _penguin.innerDistance;
    protected bool AttackInable => Vector3.Distance(_penguin.transform.position, _penguin.enemy.position) <= _penguin.attackDistance;
    protected int _animBoolHash;

    public PenguinState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName)
    {
        _penguin = penguin;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
        _navAgent = _penguin.NavAgent;
    }

    public virtual void Enter()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _penguin.Input.ClickEvent += HandleClick;
    }

    public virtual void UpdateState()
    {

    }

    private void HandleClick()
    {
        if (IsInside || AttackInable) _penguin.IsClickToMoving = true;
        _penguin.SetMovement();
        _stateMachine.ChangeState(PenguinStateEnum.Move);
    }


    public virtual void Exit()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
        _penguin.Input.ClickEvent -= HandleClick;
    }
}
