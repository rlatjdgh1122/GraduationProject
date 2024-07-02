using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuilding : BaseBuilding
{
    private BuildingUI _buildingPanel;

    protected override void Awake()
    {
        base.Awake();

        _buildingPanel = UIManager.Instance.canvasTrm.GetComponentInChildren<BuildingUI>();
        Debug.Log($"{gameObject}: {HealthCompo.maxHealth}");
        Debug.Log($"{gameObject}: {HealthCompo.currentHealth}");
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
            _buildingPanel.BuildingHealth = HealthCompo;

            _buildingPanel.SetStat();
            SignalHub.OnDefaultBuilingClickEvent?.Invoke();
        }
    }
}
