using ArmySystem;
using SynergySystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SynergyBuilding : BaseBuilding
{
    [SerializeField] private SynergyBuildingInfoDataSO _infoDataSO;
    public SynergyBuildingDeadController DeadController { get; set; }

    private BuildingUI _buildingPanel;

    private List<Ability> _ablityList = new();

    private Army _army;

    protected override void Awake()
    {
        base.Awake();

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
        if (!WaveManager.Instance.IsBattlePhase && IsInstalled)
        {
            _buildingPanel.SynergyBuilding = this;
            _buildingPanel.BuildingHealth = HealthCompo;

            _buildingPanel.SetStat();
            _buildingPanel.ShowBuildingUI(_infoDataSO);

            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }
    }

    private void OnEnable()
    {
        //군단의 시너지가 취소됐을 때
        SignalHub.OnSynergyDisableEvent += OnSynergyDisable;
        SignalHub.OnSynergyEnableEvent += OnSynergyEnable;

        //건물 부서졌을 때
        DeadController.OnBuildingDeadEvent += RemoveSynergyBuff;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        //군단의 시너지가 취소됐을 때
        SignalHub.OnSynergyDisableEvent -= OnSynergyDisable;
        SignalHub.OnSynergyEnableEvent -= OnSynergyEnable;

        //건물 부서졌을 때
        DeadController.OnBuildingDeadEvent -= RemoveSynergyBuff;
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

    public void SetSynergyBuff(Ability ability)
    {
        if (ability == null) return;
        
        _ablityList.Add(ability);

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
            if (ability.isSkillUpgrade)
            {
                _army.SkillController.LevelUp(ability.Value);
                continue;
            }
            
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
    }
}