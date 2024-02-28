using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonGenerator : MonoBehaviour
{
    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;

    private RectTransform ResourceBuildingBtns;
    private RectTransform BuffBuildingBtns;
    private RectTransform DefenseBuildingBtns;

    [SerializeField]
    private Button _buildingButtonPrefab;

    private BuildingFactory _buildingFactory;
    private SpawnUI _spawnUI;
    private ConstructionStation _constructionStation;

    private void Awake()
    {
        _constructionStation = GameObject.FindAnyObjectByType<ConstructionStation>();
        _buildingFactory = GameObject.FindAnyObjectByType<BuildingFactory>();
        ResourceBuildingBtns = transform.Find("ConstructionStationPanel/ResourceBuildingPanel/Buttons").GetComponent<RectTransform>();
        BuffBuildingBtns = transform.Find("ConstructionStationPanel/BuffBuildingPanel/Buttons").GetComponent<RectTransform>();
        DefenseBuildingBtns = transform.Find("ConstructionStationPanel/DefenseBuildingPanel/Buttons").GetComponent<RectTransform>();
        _spawnUI = GetComponent<SpawnUI>();
        GenerateBuildingBtn();
    }

    private void GenerateBuildingBtn()
    {
        foreach(var building in _buildingDatabaseSO.BuildingItems)
        {
            var button = Instantiate(_buildingButtonPrefab, Vector3.zero, Quaternion.identity).GetComponent<Button>();
            switch (building.BuildingTypeEnum)
            {
                case BuildingType.ResourceBuilding:
                    button.transform.SetParent(ResourceBuildingBtns);
                    break;
                case BuildingType.DefenseBuilding:
                    button.transform.SetParent(DefenseBuildingBtns);
                    break;
                case BuildingType.BuffBuilding:
                    button.transform.SetParent(BuffBuildingBtns);
                    break;
            }

            //button.transform.localScale = new Vector3(1.0f, 2.385f, 1.0f);
            button.AddComponent<SpawnBuildingButton>().SetUpButtonInfo(button, _buildingFactory, building, _spawnUI, _constructionStation);
        }
    }
}
