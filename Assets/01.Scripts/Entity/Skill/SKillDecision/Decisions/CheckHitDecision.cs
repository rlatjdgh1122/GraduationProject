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
        _hitCount = -_maxHitCount; //ó������ ��밡���ϰ�
    }

    private void OnDisable()
    {
        if (_actionData != null)
            _actionData.OnHitCountUpdated -= OnHitCountUpdatedHandler;
    }

    public override void SetUp(Transform parentRoot)
    {
        base.SetUp(parentRoot);

        _actionData.OnHitCountUpdated += OnHitCountUpdatedHandler;
    }

    private void OnHitCountUpdatedHandler(int value) //���������� üũ
    {
        if (_checkSkillReady == false) return;

        if (MakeDecision()) //��ų��� ������ ó�� �����Ҷ� �ѹ� ����
        {
            OnSkillReadyEvent?.Invoke();
            _checkSkillReady = false;
        }
    }

    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();

        _hitCount = 0;
        _saveHitCount = _actionData.HitCount;
        _checkSkillReady = true;
    }

    public override bool MakeDecision()
    {
        return _hitCount + _maxHitCount <= _actionData.HitCount - _saveHitCount;
    }

    public override void LevelUp()
    {
        _maxHitCount--;
    }
}
