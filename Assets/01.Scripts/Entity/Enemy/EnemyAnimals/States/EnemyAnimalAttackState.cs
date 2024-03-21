using UnityEngine;

public class EnemyAnimalAttackState : EnemyAnimalBaseState
{
    private int _comboCounter; //���� �޺� ��ġ
    private float _lastAttackTime; //���������� �����ߴ� �ð�
    private float _comboWindow = 0.3f; //�ʱ�ȭ ��Ÿ��
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public EnemyAnimalAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (_comboCounter > 3 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0; //�޺� �ʱ�ȭ ���ǿ� ���� �޺� �ʱ�ȭ
        }
        _enemy.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);

        _enemy.StopImmediately();

        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_enemy.CurrentTarget.IsDead)
        {
            _enemy.CurrentTarget = null;
        }

        if (_triggerCalled) //������ �� ���� ������ ��,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //���� -> �ؼ����� Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        ++_comboCounter;
        _lastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
