using UnityEngine;
using UnityEngine.AI;

public class State : MonoBehaviour, IState
{
    protected TestStateMachine _stateMachine;
    protected Penguin _penguin;
    protected NavMeshAgent _navAgent; //편의를 위해서 여기에도 NavAgent 선언
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
        _penguin.AnimatorCompo.SetBool(_animBoolHash, true); //들어오면 내 애니메이션을 활성화 해주는 것
        _navAgent = _penguin.NavAgent;
    }

    public virtual void UpdateState()
    {
        
    }

    public virtual void ExitState()
    {
        _penguin.AnimatorCompo.SetBool(_animBoolHash, false); //나갈땐 꺼줌
    }

    public virtual void AnimationEndTrigger()
    {
        _triggerCalled = true;
    }
}