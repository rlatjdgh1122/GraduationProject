using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct BuildingInfo
{
    [field: SerializeField]
    public int ID { get ; private set; }
    [HideInInspector]
    public Grid GridCompo;
    [field: SerializeField]
    public Material TransparencyMat { get; private set; }
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : WorkableObject
{
    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;

    [SerializeField]
    private TextMeshProUGUI _installedFinText;

    private BuildingItemInfo _buildingItemInfo;

    public BuildingInfo BuildingInfoCompo;
    private bool isInstalled = false;
    public bool IsInstalled => isInstalled;

    private Material[] _meshNormalMats;
    private MeshRenderer[] _meshRenderers;

    private Material[] _skinNormalMats;
    private SkinnedMeshRenderer[] _skinRenderers;

    private int installedTime = 0;

    private bool isInstalling = false;
    public bool IsInstalling => isInstalling;

    protected override void Awake()
    {
        try
        {
            _buildingItemInfo = _buildingDatabaseSO.BuildingItems.Find(idx => idx.ID == BuildingInfoCompo.ID);
        }
        catch
        {
            Debug.LogError($"Not Founded id: {gameObject}");
        }
        SetUpCompo();
    }

    private void SetUpCompo()
    {
        _meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        _skinRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        _meshNormalMats = new Material[_meshRenderers.Length];
        _skinNormalMats = new Material[_skinRenderers.Length];

        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshNormalMats[i] = _meshRenderers[i].material;
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            _skinNormalMats[i] = _skinRenderers[i].material;
        }

        BuildingInfoCompo.GridCompo = GetComponent<Grid>();
    }

    public void Installed()
    {
        if (_buildingItemInfo != null)
        {
            WaveManager.Instance.OnBattlePhaseEndEvent += PlusInstalledTime;
        }
        else
        {
            SetInstalled();
        }

        isInstalling = true;
    }

    private void PlusInstalledTime()
    {
        installedTime++;

        if (installedTime >= _buildingItemInfo.InstalledTime)
        {
            WaveManager.Instance.OnBattlePhaseEndEvent -= PlusInstalledTime;
            SetInstalled();
        }
    }

    public void SetSelect()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = BuildingInfoCompo.TransparencyMat;
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            _skinRenderers[i].material = BuildingInfoCompo.TransparencyMat;
        }

    }

    public void SetInstalled()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = _meshNormalMats[i];
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            _skinRenderers[i].material = _skinNormalMats[i];
        }
        isInstalled = true;
        isInstalling = false;

        if (_buildingItemInfo != null)
        {
            UIManager.Instance.InitializHudTextSequence();
            _installedFinText.SetText($"{_buildingItemInfo.Name: ��ġ �Ϸ�!}");
            UIManager.Instance.SpawnHudText(_installedFinText);
        }
    }

    protected virtual void Update()
    {
        if(isInstalled)
        {
            Running(); // ��ġ �Ǹ� ���� ����
        }
    }

    protected abstract void Running();
}
