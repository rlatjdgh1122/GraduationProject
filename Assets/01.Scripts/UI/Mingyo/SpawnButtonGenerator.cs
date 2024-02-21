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

    private RectTransform _buildingBtnParent;

    [SerializeField]
    private Button _buttonPrefab;

    private BuildingFactory _buildingFactory;
    private SpawnUI _spawnUI;
    private ConstructionStation _constructionStation;

    private void Awake()
    {
        _constructionStation = GameObject.FindAnyObjectByType<ConstructionStation>();
        _buildingFactory = GameObject.FindAnyObjectByType<BuildingFactory>();
        _buildingBtnParent = transform.Find("BuildingPanel/Buttons").GetComponent<RectTransform>();
        _spawnUI = GetComponent<SpawnUI>();
        GenerateBuildingBtn();
    }

    private void GenerateBuildingBtn()
    {
        foreach(var building in _buildingDatabaseSO.BuildingItems)
        {
            var button = Instantiate(_buttonPrefab, Vector3.zero, Quaternion.identity);
            button.transform.SetParent(_buildingBtnParent);
            button.transform.localScale = new Vector3(1.0f, 2.385f, 1.0f);
            button.AddComponent<SpawnBuildingButton>().SetUpButtonInfo(button, _buildingFactory, building, _spawnUI, _constructionStation);
        }
    }
}
