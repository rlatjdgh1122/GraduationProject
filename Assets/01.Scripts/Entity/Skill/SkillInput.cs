using UnityEngine;

public class SkillInput : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;

    private General _curSelectGeneral = null;

    private bool CanPlaySkill()
    {
        return _curSelectGeneral != null; /* && ����� �ϱ� ���� �ּ� ó����. ���߿��� �ּ� Ǯ ��.
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

    public void SelectGeneral(General general) //ArmyManager�� OnChangedArmy���� ���õ� General�� ����
    {
        _curSelectGeneral = general;
    }

    private void OnDisable()
    {
        _inputReader.OnSkillEvent -= OnPlaySkill;
        _inputReader.OnUltimateEvent -= OnPlayUltimateSkill;
    }
}
