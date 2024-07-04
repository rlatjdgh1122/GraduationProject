using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuilding : BaseBuilding
{
    public SynergyBuildingDeadController DeadController { get; set; }

    private BuildingUI _buildingPanel;

    protected override void Awake()
    {
        base.Awake();

        _buildingPanel = UIManager.Instance.canvasTrm.GetComponentInChildren<BuildingUI>();
        DeadController = GetComponent<SynergyBuildingDeadController>();
    }

    protected override void Running()
    {

    }

    private bool _isFirst = false;
    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase && _isFirst)
        {
            UIManager.Instance.ShowPanel("BuildingUI");
            _buildingPanel.BuildingStat = (SynergyBuildingStat)_characterStat;
            _buildingPanel.BuildingHealth = HealthCompo;
            _buildingPanel.SynergyBuilding = this;

            _buildingPanel.SetStat();
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }

        _isFirst = true;
    }
}
