using ArmySystem;
using System;
using UnityEngine;

public class SkillInput : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    private Army _selectArmy = null;

    private bool CanPlaySkill()
    {
        return _selectArmy.General != null; /* && 디버깅 하기 위해 주석 처리함. 나중에는 주석 풀 것.
               WaveManager.Instance.IsBattlePhase; */
    }

    private void OnEnable()
    {
        SignalHub.OnArmyChanged += OnArmyChangedEventHandler;
        _inputReader.OnSkillEvent += OnPlaySkill;
        _inputReader.OnUltimateEvent += OnPlayUltimateSkill;
    }

    private void OnArmyChangedEventHandler(Army prevArmy, Army newArmy)
    {
        _selectArmy = newArmy;
    }

    private void OnPlaySkill()
    {
        if (!CanPlaySkill()) { return; }

        if (_selectArmy.General.Skill.SkillController.CheckDecision())
        {
            _selectArmy.General.OnSkillEvent();

            _selectArmy.General.Skill.SkillController.OnUsed();
        }
    }

    private void OnPlayUltimateSkill()
    {
        if (!CanPlaySkill() || !_selectArmy.General.IsSynergy) { return; }
        //Debug.Log(_curSelectGeneral.Ultimate.SkillController.CheckDecision());
        if (_selectArmy.General.Ultimate.SkillController.CheckDecision())
        {
            _selectArmy.General.OnUltimateEvent();

            _selectArmy.General.Ultimate.SkillController.OnUsed();
        }
    }

    public void SelectGeneral(General general) //ArmyManager의 OnChangedArmy에서 선택된 General을 받음
    {
        //_curSelectGeneral = general;
    }

    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedEventHandler;
        _inputReader.OnSkillEvent -= OnPlaySkill;
        _inputReader.OnUltimateEvent -= OnPlayUltimateSkill;
    }
}
