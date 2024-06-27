using System;
using UnityEngine;

public class CheckHitDecision : SKillDecision
{
    public int InitMaxHitCount = 10;

    private int _maxHitCount = 3;
    private int _hitCount = 0;
    private int _saveHitCount = 0;

    private bool _checkSkillReady = true;

    private void Start()
    {
        _maxHitCount = InitMaxHitCount;
        _hitCount = -_maxHitCount; //처음에도 사용가능하게
    }

    private void OnDisable()
    {
        if (_entityActionData)
            _entityActionData.OnHitCountUpdated -= OnHitCountUpdatedHandler;
    }

    public override void SetUp(Transform parentRoot)
    {
        base.SetUp(parentRoot);

        _entityActionData.OnHitCountUpdated += OnHitCountUpdatedHandler;
    }

    private void OnHitCountUpdatedHandler(int value) //맞을때마다 체크
    {
        if (!_checkSkillReady) return;

        if (MakeDecision()) //스킬사용 조건이 처음 만족할때 한번 실행
        {
            _skillActionData.AddSkillUsedCount();
            OnSkillReadyEvent?.Invoke();
            _checkSkillReady = false;
        }
    }

    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();

        _hitCount = 0;
        _saveHitCount = _entityActionData.HitCount;
        _checkSkillReady = true;
    }

    public override bool MakeDecision()
    {
        return _hitCount + _maxHitCount <= _entityActionData.HitCount - _saveHitCount;
    }

    public override void LevelUp()
    {
        _maxHitCount--;
    }
}
