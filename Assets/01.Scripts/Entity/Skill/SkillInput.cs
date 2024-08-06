using ArmySystem;
using System;
using UnityEngine;

public class SkillInput : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    private Army _selectArmy = null;

    private bool CanPlaySkill()
    {
        if (_selectArmy == null) return false;
        if (_selectArmy.IsHealing)
        {
            UIManager.Instance.ShowWarningUI("회복 중에는 스킬을 쓸 수 없습니다.");
            return false;
        }

        return _selectArmy.General != null && WaveManager.Instance.IsBattlePhase;

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
