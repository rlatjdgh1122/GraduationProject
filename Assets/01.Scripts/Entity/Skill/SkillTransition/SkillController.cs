using SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    [SerializeField] private SKillDecision _skillDecision = null;
    [SerializeField] private List<UnvariableSkillDecision> _unvariableDecisions = new();

    private List<ISkillDecision> _allDecisions = new();

    // 스킬 사용 시 발생하는 이벤트
    public Action OnSkillUsedEvent = null;

    // 스킬 액션 발동 시 발생하는 이벤트
    public Action OnSkillActionEnterEvent = null;

    //스킬 조건의 수치를 변경해줄때 발생하는 이벤트
    public Action<int> OnChangedMaxValueEvent = null;

    private void Awake()
    {
        _skillDecision = GetComponent<SKillDecision>();
        GetComponents(_unvariableDecisions);
        GetComponents(_allDecisions);

    }

    public void SetUp(Transform parentRoot)
    {
        foreach (ISkillDecision decision in _allDecisions)
        {
            decision.SetUp(parentRoot);

        }//end foreach

        OnRegister();
    }

    public bool CheckDecision()
    {
        bool result = false;

        foreach (ISkillDecision decision in _allDecisions)
        {
            result = decision.MakeDecision();

            if (result == false) break;

        }//end foreach

        return result;
    }

    public void OnUsed()
    {
        foreach (ISkillDecision decision in _allDecisions)
        {
            decision.OnUsed();

        }//end foreach
    }

    public void LevelUp(int value = 1)
    {
        _skillDecision.LevelUp(value);
    }

    public void OnRegister()
    {
        OnChangedMaxValueEvent += _skillDecision.OnChangedMaxValueEvent;
        OnSkillActionEnterEvent += OnSkillActionEnterEvent;
        OnSkillUsedEvent += OnSkillUsedEvent;
    }

    public void OffRegister()
    {
        OnChangedMaxValueEvent -= _skillDecision.OnChangedMaxValueEvent;
        OnSkillActionEnterEvent -= OnSkillActionEnterEvent;
        OnSkillUsedEvent -= OnSkillUsedEvent;
    }

    private void OnDisable()
    {
        OffRegister();
    }

}
