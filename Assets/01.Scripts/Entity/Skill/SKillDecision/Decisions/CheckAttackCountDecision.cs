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
        _attackCount = -_maxAttackCount; // 처음에도 사용 가능하게 초기화
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

    private void AttackCountHandler(int value) // 공격 횟수 업데이트 시 체크
    {
        if (!_checkSkillReady) return;

        if (MakeDecision()) // 스킬 사용 조건이 처음 만족할 때 한 번 실행
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
        _maxAttackCount--; // 레벨업 시 최대 공격 횟수를 줄여 스킬을 더 자주 사용할 수 있도록 함
    }
}
