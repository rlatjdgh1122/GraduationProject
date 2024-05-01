using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NexusManager : Singleton<NexusManager>
{
    [SerializeField]
    private NexusStat _nexusStat;
    [SerializeField]
    private BuildingDatabaseSO _buildingDatabase;
    [SerializeField]
    private NexusInfoDataSO _nexusInfo;
    [SerializeField]
    private NexusBase _nexusBase;

    public NexusStat NexusStat => _nexusStat;
    public BuildingDatabaseSO BuildingDatabase => _buildingDatabase;
    public NexusInfoDataSO NexusInfo => _nexusInfo;
    public NexusBase NexusBase => _nexusBase;

    public override void Awake()
    {
        _nexusStat = Instantiate(_nexusStat);
        _buildingDatabase = Instantiate(_buildingDatabase);   
        _nexusInfo = Instantiate(_nexusInfo);

        SetNexusInfoData();
    }

    public void SetNexusHealth()
    {
        _nexusBase.HealthCompo.SetMaxHealth(_nexusStat); //최대 체력 올려주고
        int healValue = (int)(_nexusStat.maxHealth.GetValue() * 0.2f);
        Debug.Log(healValue);
        _nexusBase.HealthCompo.ApplyHeal(healValue); //최대 체력의 20%만큼 힐
    }

    private void SetNexusInfoData()
    {
        _nexusInfo.currentMaxHealth = _nexusStat.GetMaxHealthValue();
        _nexusInfo.previousMaxHealth = 0;
        _nexusInfo.nextMaxHealth = _nexusStat.GetUpgradedMaxHealthValue();
        _nexusInfo.currentWorkerCount = WorkerManager.Instance.MaxWorkerCount;
        _nexusInfo.previousWorkerCount = 0;
        _nexusInfo.nextWorkerCount = WorkerManager.Instance.MaxWorkerCount + 1;
        _nexusInfo.previewBuilding = _buildingDatabase.BuildingItems.FirstOrDefault(b => b.UnlockedLevel == _nexusStat.level + 1);
        _nexusInfo.unlockedBuilding = null;
    }

    public void UpdateNexusInfoData()
    {
        _nexusInfo.previousMaxHealth = _nexusInfo.currentMaxHealth;
        _nexusInfo.previousWorkerCount = _nexusInfo.currentWorkerCount;
        _nexusInfo.unlockedBuilding = _nexusInfo.previewBuilding;

        _nexusInfo.currentMaxHealth = _nexusInfo.nextMaxHealth;
        _nexusInfo.currentWorkerCount = _nexusInfo.nextWorkerCount;

        _nexusInfo.nextMaxHealth = _nexusStat.GetUpgradedMaxHealthValue();
        _nexusInfo.nextWorkerCount = WorkerManager.Instance.MaxWorkerCount + 1;
        _nexusInfo.previewBuilding = _buildingDatabase.BuildingItems.FirstOrDefault(b => b.UnlockedLevel == _nexusStat.level + 1);
    }
}