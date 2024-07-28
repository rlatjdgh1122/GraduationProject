using UnityEngine;
using UnityEngine.AI;

public class EnemyState
{
    protected EnemyStateMachine _stateMachine;

    protected Enemy _enemy;
    protected NavMeshAgent _navAgent;
    protected EntityActionData _entityActionData;
    protected int _animBoolHash;
    protected bool _triggerCalled = true;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        _enemy = enemy;
        _stateMachine = stateMachine;

        _navAgent = enemy.GetComponent<NavMeshAgent>();
        _entityActionData = enemy.GetComponent<EntityActionData>();

        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    #region Base
    public virtual void EnterState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, true); //������ �� �ִϸ��̼��� Ȱ��ȭ ���ִ� ��
        _enemy.FindNearestTarget();
    }

    public virtual void UpdateState()
    {

    }

    public virtual void ExitState()
    {
        _enemy.AnimatorCompo.SetBool(_animBoolHash, false); //������ ����
    }

    public virtual void AnimationTrigger()
    {
        _triggerCalled = true;
    }
    #endregion 
}
