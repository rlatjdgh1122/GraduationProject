using UnityEngine;

public class SkillInput : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    private General _curSelectGeneral = null;

    private bool CanPlaySkill()
    {
        return _curSelectGeneral != null; /* && 디버깅 하기 위해 주석 처리함. 나중에는 주석 풀 것.
               WaveManager.Instance.IsBattlePhase; */
    }

    private void OnEnable()
    {
        _inputReader.OnSkillEvent += OnPlaySkill;
        _inputReader.OnUltimateEvent += OnPlayUltimateSkill;
    }

    private void OnPlaySkill()
    {
        if (!CanPlaySkill()) { return; }

        if (_curSelectGeneral.Skill.SkillController.CheckDecision())
        {
            _curSelectGeneral.OnSkillEvent();

            _curSelectGeneral.Skill.SkillController.OnUsed();
        }
    }

    private void OnPlayUltimateSkill()
    {
        if (!CanPlaySkill() || !_curSelectGeneral.IsSynergy) { return; }
        Debug.Log(_curSelectGeneral.Ultimate.SkillController.CheckDecision());
        if (_curSelectGeneral.Ultimate.SkillController.CheckDecision())
        {
            _curSelectGeneral.OnUltimateEvent();

            _curSelectGeneral.Ultimate.SkillController.OnUsed();
        }
    }

    public void SelectGeneral(General general) //ArmyManager의 OnChangedArmy에서 선택된 General을 받음
    {
        _curSelectGeneral = general;
    }

    private void OnDisable()
    {
        _inputReader.OnSkillEvent -= OnPlaySkill;
        _inputReader.OnUltimateEvent -= OnPlayUltimateSkill;
    }
}
