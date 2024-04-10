using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour, IState
{
    protected TestStateMachine _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent; //���Ǹ� ���ؼ� ���⿡�� NavAgent ����
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public void SetUp(Penguin penguin, TestStateMachine stateMachine, string animationBoolName)
    {
        _penguin = penguin;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animationBoolName);
        _navAgent = _penguin.NavAgent;
    }

    public virtual void EnterState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _navAgent = _penguin.NavAgent;
    }

    public virtual void UpdateState()
    {
        
    }

    public virtual void ExitState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationEndTrigger()
    {
        _triggerCalled = true;
    }
}