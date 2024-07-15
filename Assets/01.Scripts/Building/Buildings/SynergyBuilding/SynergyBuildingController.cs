using SynergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuildingController : MonoBehaviour
{
    [SerializeField]
    private List<SynergyBuilding> _synergyBuildings = new List<SynergyBuilding>();

    BuildingFactory _buildingFactory;

    private void Awake()
    {
        _buildingFactory = FindObjectOfType<BuildingFactory>();
    }

    private void OnEnable()
    {
        SignalHub.OnSynergyEnableEvent += OnSynergyEnable;
    }

    private void OnDisable()
    {
        SignalHub.OnSynergyEnableEvent -= OnSynergyEnable;
    }

    private void OnSynergyEnable(SynergyType synergyType)
    {
        foreach(SynergyBuilding synergyBuilding in _synergyBuildings)
        {
            if (synergyBuilding.BuildingSynergyType == synergyType)
            {
                BuildingView view = GameObject.Find(synergyType.ToString()).GetComponent<BuildingView>();

                view.spawn.SetUpButtonInfo(view.purchaseButton, _buildingFactory, view.Building);
                view.lockedPanel.gameObject.SetActive(false);
            }
        }
        
    }
}
