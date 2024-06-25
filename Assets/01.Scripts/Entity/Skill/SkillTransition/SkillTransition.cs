using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTransition : MonoBehaviour
{
    [SerializeField]
    private SKillDecision _decision;

    // 스킬 사용 시 발생하는 이벤트
    public Action OnSkillUsedEvent = null;

    // 스킬 사용 가능 시 발생하는 이벤트
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
