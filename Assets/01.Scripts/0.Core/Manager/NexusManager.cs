using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusManager : Singleton<NexusManager>
{
    [SerializeField]
    private NexusStat _nexusStat;
    [SerializeField]
    private BuildingDatabaseSO _buildingDatabase;

    public NexusStat NexusStat => _nexusStat;
    public BuildingDatabaseSO BuildingDatabase => _buildingDatabase;

    public override void Awake()
    {
        _nexusStat = Instantiate(_nexusStat);
        _buildingDatabase = Instantiate(_buildingDatabase);   
    }
}