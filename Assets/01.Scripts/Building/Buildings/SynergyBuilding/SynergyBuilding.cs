using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuilding : BaseBuilding
{
    private BuildingUI _buildingPanel;
    private Health _health;

    protected override void Awake()
    {
        base.Awake();

        _buildingPanel = UIManager.Instance.canvasTrm.GetComponentInChildren<BuildingUI>();
        _health = GetComponent<Health>();

        _health.SetHealth(_characterStat);
    }

    protected override void Running()
    {

    }

    private void OnMouseDown()
    {
        if (!WaveManager.Instance.IsBattlePhase)
        {
            UIManager.Instance.ShowPanel("BuildingUI");
            _buildingPanel.BuildingStat = (SynergyBuildingStat)_characterStat;
            _buildingPanel.BuildingHealth = _health;

            _buildingPanel.SetStat();
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }
    }
}
