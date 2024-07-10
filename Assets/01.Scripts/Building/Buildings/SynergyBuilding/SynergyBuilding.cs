using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuilding : BaseBuilding
{
    [SerializeField] private SynergyBuildingInfoDataSO _infoDataSO;
    public SynergyBuildingDeadController DeadController { get; set; }

    private BuildingUI _buildingPanel;

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

    private bool _isFirst = false;
    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && _isFirst)
        {
            _buildingPanel.SynergyBuilding = this;
            _buildingPanel.BuildingHealth = HealthCompo;

            _buildingPanel.SetStat();
            _buildingPanel.ShowBuildingUI(_infoDataSO);

            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }

        _isFirst = true;
    }
}
