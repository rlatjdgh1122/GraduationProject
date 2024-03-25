using UnityEngine;

public class EnemyAnimalAttackState : EnemyAnimalBaseState
{
    private AnimalAttackableEntity _animalAttack;
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public EnemyAnimalAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
        _animalAttack = enemyBase.GetComponent<AnimalAttackableEntity>();
    }

    public override void Enter()
    {
        base.Enter();
        if (_animalAttack.ComboCounter > _animalAttack.animalAttack.Count - 1
            || Time.time >= _animalAttack.LastAttackTime + _animalAttack.ComboWindow)
        {
            _animalAttack.ComboCounter = 0; //�޺� �ʱ�ȭ ���ǿ� ���� �޺� �ʱ�ȭ
        }
        _enemy.AnimatorCompo.SetInteger(_comboCounterHash, _animalAttack.ComboCounter);

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
        ++_animalAttack.ComboCounter;
        _animalAttack.LastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
