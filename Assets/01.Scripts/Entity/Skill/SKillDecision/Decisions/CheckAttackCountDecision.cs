using System;
using UnityEngine;

public class CheckAttackCountDecision : SKillDecision
{
    public int InitMaxAttackCount = 5;

    private int _maxAttackCount;
    private int _attackCount = 0;
    private int _saveAttackCount = 0;

    private bool _checkSkillReady = true;

    private void Start()
    {
        _maxAttackCount = InitMaxAttackCount;
        _attackCount = -_maxAttackCount; // ó������ ��� �����ϰ� �ʱ�ȭ
    }

    private void OnDisable()
    {
        if (_entityActionData)
            _entityActionData.OnAttackCountUpdated -= AttackCountHandler;
    }

    public override void SetUp(Transform parentRoot)
    {
        base.SetUp(parentRoot);

        _entityActionData.OnAttackCountUpdated += AttackCountHandler;
    }

    private void AttackCountHandler(int value) // ���� Ƚ�� ������Ʈ �� üũ
    {
        if (!_checkSkillReady) return;

        if (MakeDecision()) // ��ų ��� ������ ó�� ������ �� �� �� ����
        {
            _skillActionData.AddSkillUsedCount();
            OnSkillReadyEvent?.Invoke();
            _checkSkillReady = false;
        }
    }

    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();

        _attackCount = 0;
        _saveAttackCount = _entityActionData.AttackCount;
        _checkSkillReady = true;
    }

    public override bool MakeDecision()
    {
        return _attackCount + _maxAttackCount <= _entityActionData.AttackCount - _saveAttackCount;
    }

    public override void LevelUp()
    {
        _maxAttackCount--; // ������ �� �ִ� ���� Ƚ���� �ٿ� ��ų�� �� ���� ����� �� �ֵ��� ��
    }
}
