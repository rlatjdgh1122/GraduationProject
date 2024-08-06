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

    public Action OnSkillReadyEvent = null;

    // 스킬 사용 시 발생하는 이벤트
    public Action OnSkillUsedEvent = null;

    // 스킬 액션 발동 시 발생하는 이벤트
    public Action OnSkillActionEnterEvent = null;

    //스킬 조건의 수치를 변경해줄때 발생하는 이벤트
    public Action<int> OnChangedMaxValueEvent = null;

    private int _desicionMaxValue = 0;

    public int GetMaxValue => _desicionMaxValue;

    private void Awake()
    {
        _skillDecision = GetComponent<SKillDecision>();
        GetComponents(_unvariableDecisions);
        GetComponents(_allDecisions);

    }

    public void Init()
    {

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
        if (_skillDecision)
        {
            _skillDecision.OnChangedMaxValueEvent += OnChangedMaxValue;
            _skillDecision.OnSkillActionEnterEvent += OnSkillActionEnter;
            _skillDecision.OnSkillUsedEvent += OnSkillUsed;
            _skillDecision.OnSkillReadyEvent += OnSkillReady;
        }
       
    }

    private void OnChangedMaxValue(int value)
    {
        _desicionMaxValue = value;

        OnChangedMaxValueEvent?.Invoke(value);
    }

    private void OnSkillActionEnter()
    {
        OnSkillActionEnterEvent?.Invoke();
    }
    private void OnSkillUsed()
    {
        OnSkillUsedEvent?.Invoke();
    }

    private void OnSkillReady()
    {
        OnSkillReadyEvent?.Invoke();
    }


    private void OffRegister()
    {
        if (_skillDecision)
        {
            _skillDecision.OnChangedMaxValueEvent -= OnChangedMaxValueEvent;
            _skillDecision.OnSkillActionEnterEvent -= OnSkillActionEnterEvent;
            _skillDecision.OnSkillUsedEvent -= OnSkillUsedEvent;
            _skillDecision.OnSkillReadyEvent -= OnSkillReadyEvent;
        }
    }

    private void OnDisable()
    {
        OffRegister();
    }

}
