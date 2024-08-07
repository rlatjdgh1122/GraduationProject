using ArmySystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SynergyBuilding : BaseBuilding
{
    [SerializeField] private SynergyBuildingInfoDataSO _infoDataSO;

    [SerializeField] private UnityEvent<int> OnHealingTimeLevelUpEvent = null;
    public int Level = 0;

    public SynergyType BuildingSynergyType => _infoDataSO.SynergyType;


    public Outline OutlineCompo { get; set; } = null;
    public SynergyBuildingDeadController DeadController { get; set; }

    private BuildingUI _buildingPanel;

    private List<Ability> _ablityList = new();

    private Army _army;

    protected override void Awake()
    {
        base.Awake();

        OutlineCompo = GetComponent<Outline>();
        DeadController = GetComponent<SynergyBuildingDeadController>();
    }

    protected override void Start()
    {
        _buildingPanel = UIManager.Instance.GetPopupUI<BuildingUI>("BuildingUI");
    }

    protected override void Running()
    {

    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && Installing)
        {
            _buildingPanel.SynergyBuilding = this;
            _buildingPanel.BuildingHealth = HealthCompo;

            _buildingPanel.SetStat();
            _buildingPanel.ShowBuildingUI(_infoDataSO);

            SignalHub.OnDefaultBuilingClickEvent?.Invoke();

            if (_army == null)
            {
                _army = ArmyManager.Instance.GetArmyBySynergyType(_infoDataSO.SynergyType);
            }
        }
    }

    private void OnEnable()
    {
        //군단의 시너지가 취소됐을 때
        SignalHub.OnSynergyDisableEvent += OnSynergyDisable;
        SignalHub.OnSynergyEnableEvent += OnSynergyEnable;

        //건물 부서졌을 때
        DeadController.OnBuildingDeadEvent += RemoveSynergyBuff;
        DeadController.OnBuildingRepairEvent += OnRepair;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        //군단의 시너지가 취소됐을 때
        SignalHub.OnSynergyDisableEvent -= OnSynergyDisable;
        SignalHub.OnSynergyEnableEvent -= OnSynergyEnable;

        //건물 부서졌을 때
        DeadController.OnBuildingDeadEvent -= RemoveSynergyBuff;
        DeadController.OnBuildingRepairEvent -= OnRepair;
    }

    public void OnSynergyDisable(SynergyType type)
    {
        if (type == _infoDataSO.SynergyType || !HealthCompo.IsDead)
        {
            RemoveSynergyBuff();
        }
    }

    public void OnSynergyEnable(SynergyType type)
    {
        if (type == _infoDataSO.SynergyType || !HealthCompo.IsDead)
        {
            _army = ArmyManager.Instance.GetArmyBySynergyType(_infoDataSO.SynergyType);
            AddSynergyBuff();
        }
    }

    public void LevelUpHealingTime(int value)
    {
        OnHealingTimeLevelUpEvent?.Invoke(value);
    }

    public void LevelUpSkillTime(int value)
    {
        _army.SkillController.LevelUp(value);
    }

    public void LevelUpUltimateTime(int value)
    {
        _army.UltimateController.LevelUp(value);
    }


    protected override void OnInstalled()
    {
        /* //설치 다되었을때
         for (int i = 0; i < Level; i++)
         {
             if (_infoDataSO[i].ReduceSkillTime > 0)
             {
                 LevelUpSkillTime(_infoDataSO[i].ReduceSkillTime);
             }
         }*/
        OnRepair();
    }


    public void OnRepair()
    {
        //스킬
        for (int i = 0; i < Level; i++)
        {
            if (_infoDataSO[i].ReduceSkillTime > 0)
            {
                LevelUpSkillTime(_infoDataSO[i].ReduceSkillTime);
            }
            if (_infoDataSO[i].ReduceUltimateTime > 0)
            {
                LevelUpUltimateTime(_infoDataSO[i].ReduceUltimateTime);
            }
        }

        //방어구 추가
        if (_ablityList.Count >= _infoDataSO.BuildingAbilityList.Count)
        {
            OnPenguinArmor();
        }

        //스탯 추가
        AddSynergyBuff();
    }

    public void SetSynergyBuff(Ability ability)
    {
        if (ability == null) return;

        _ablityList.Add(ability);

        if (_ablityList.Count >= _infoDataSO.BuildingAbilityList.Count)
        {
            OnPenguinArmor();
        }

        AddSynergyBuff();
    }

    public void AddSynergyBuff()
    {
        if (_ablityList.Count == 0 || _army == null) return;

        foreach (var ability in _ablityList)
        {
            _army?.RemoveStat(ability);
        }

        foreach (var ability in _ablityList)
        {
            _army?.AddStat(ability);
        }
    }

    public void RemoveSynergyBuff()
    {
        if (_ablityList.Count == 0 || _army == null) return;

        foreach (var ability in _ablityList)
        {
            _army?.RemoveStat(ability);
        }

        //이거 함수 나눠주는게 좋을 것 같은데

        //부서지면 스킬 쿨타임 감소시킨거 취소해줌
        for (int i = 0; i < Level; i++)
        {
            if (_infoDataSO[i].ReduceSkillTime > 0)
            {
                LevelUpSkillTime(-_infoDataSO[i].ReduceSkillTime);
            }
            if (_infoDataSO[i].ReduceUltimateTime > 0)
            {
                LevelUpUltimateTime(-_infoDataSO[i].ReduceUltimateTime);
            }
        }

        OffPenguinArmor();
    }


    public void OnPenguinArmor()
    {
        Army army = ArmyManager.Instance.GetArmyBySynergyType(BuildingSynergyType);

        foreach (var a in army.Soldiers)
        {
            Penguin penguin = a as Penguin;
            if (penguin != null)
            {
                penguin.ArmorOn();
            }

            DummyPenguin dummy = PenguinManager.Instance.GetDummyByPenguin(a);
            if (dummy != null)
            {
                dummy.ArmorOn();
            }
        }
    }

    public void OffPenguinArmor()
    {
        Army army = ArmyManager.Instance.GetArmyBySynergyType(BuildingSynergyType);

        foreach (var a in army.Soldiers)
        {
            Penguin penguin = a as Penguin;
            if (penguin != null)
            {
                penguin.ArmorOff();
            }

            DummyPenguin dummy = PenguinManager.Instance.GetDummyByPenguin(a);
            if (dummy != null)
            {
                dummy.ArmorOff();
            }
        }
    }
}