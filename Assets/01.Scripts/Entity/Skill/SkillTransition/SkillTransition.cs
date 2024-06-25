using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTransition : MonoBehaviour
{
    [SerializeField]
    private SKillDecision _decision;

    // ��ų ��� �� �߻��ϴ� �̺�Ʈ
    public Action OnSkillUsedEvent = null;

    // ��ų ��� ���� �� �߻��ϴ� �̺�Ʈ
    public Action OnSkillReadyEvent = null;


    private void Awake()
    {
        _decision = GetComponent<SKillDecision>();
    }

    public void SetUp(Transform parentRoot)
    {
        _decision.SetUp(parentRoot);
    }

    public bool CheckDecision()
    {
        return _decision.MakeDecision();
    }

    public void Reset()
    {
        _decision.ResetValue();
    }

    public float GetDecisionValue()
    {
        return _decision.GetDecisionValue();
    }
}
