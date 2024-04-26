using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    [field: SerializeField]
    public Material GreenTransparencyMat { get; private set; }
    [field: SerializeField]
    public Material RedTransparencyMat { get; private set; }
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : WorkableObject
{
    [SerializeField]
    private InputReader _inputReader;
    protected InputReader InputReaderCompo => _inputReader;

    [SerializeField]
    private BuildingDatabaseSO _buildingDatabaseSO;
    public BuildingDatabaseSO BuildingDatabaseSO => _buildingDatabaseSO;

    [SerializeField]
    private TextMeshProUGUI _installedFinText;

    private BuildingItemInfo _buildingItemInfo;
    public BuildingItemInfo BuildingItemInfoCompo => _buildingItemInfo;

    public BuildingInfo BuildingInfoCompo;
    private bool isInstalled = false;
    public bool IsInstalled => isInstalled;

    private Material[] _meshNormalMats;
    protected MeshRenderer[] _meshRenderers;

    private Material[] _skinNormalMats;
    protected SkinnedMeshRenderer[] _skinRenderers;

    private int installedTime = 0;

    private bool isInstalling = false;
    public bool IsInstalling => isInstalling;

    private TimeRemain _remainTimeUI;
    public TimeRemain RemainTimeUI => _remainTimeUI;

    private bool isSelected;
    public bool IsSelected => isSelected;

    protected LayerMask _groundLayer = 1 << 3;

    private BattlePhaseStartEvent _phaseStartSubscriptionAction;

    protected Ground InstalledGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            return hit.collider.GetComponent<Ground>();
        }
        return null;
    }


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

        if (_buildingItemInfo != null)
        {
            if (_buildingItemInfo.BuildingTypeEnum != BuildingType.Trap)
            {
                _remainTimeUI = transform.Find("TimeRemainCanvas").GetComponent<TimeRemain>();
            }
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
        if (_buildingItemInfo != null && _buildingItemInfo.InstalledTime > 0)
        {
            ChangeToTransparencyMat(OutlineColorType.None);
            WorkerManager.Instance.SendBuilders(_buildingItemInfo.NecessaryResourceCount, this);

            _phaseStartSubscriptionAction = () => WorkerManager.Instance.ReturnBuilders(this);
            SignalHub.OnBattlePhaseStartEvent += _phaseStartSubscriptionAction;
            SignalHub.OnBattlePhaseEndEvent += PlusInstalledTime;
            _remainTimeUI.OnRemainUI();
            _remainTimeUI.SetText((int)_buildingItemInfo.InstalledTime);
        }
        else
        {
            SetInstalled();
        }

        isInstalling = true;
        StopInstall();
        InstalledGround()?.InstallBuilding();
    }

    private void PlusInstalledTime()
    {
        installedTime++;
        _remainTimeUI.SetText((int)_buildingItemInfo.InstalledTime - installedTime);

        if (installedTime >= _buildingItemInfo.InstalledTime)
        {
            SignalHub.OnBattlePhaseEndEvent -= PlusInstalledTime;
            SetInstalled();
            _remainTimeUI.OffRemainUI();
        }
        else
        {
            WorkerManager.Instance.SendBuilders(_buildingItemInfo.NecessaryResourceCount, this);
        }
    }

    public virtual void SetSelect()
    {
        isSelected = true;
    }

    protected virtual void SetInstalled()
    {
        MatChangeToNormal();

        isInstalled = true;
        isInstalling = false;

        if (_buildingItemInfo != null)
        {
            //_installedFinText.SetText($"{_buildingItemInfo.Name: 설치 완료!}");
            //UIManager.Instance.SpawnHudText(_installedFinText);

            SignalHub.OnBattlePhaseStartEvent -= _phaseStartSubscriptionAction;
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

    public virtual void StopInstall()
    {
        isSelected = false;
    }

    public void ChangeToTransparencyMat(OutlineColorType colorType)
    {
        if (colorType == OutlineColorType.Green)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material = BuildingInfoCompo.GreenTransparencyMat;
            }

            for (int i = 0; i < _skinRenderers.Length; i++)
            {
                _skinRenderers[i].material = BuildingInfoCompo.GreenTransparencyMat;
            }
        }
        else if (colorType == OutlineColorType.Red)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material = BuildingInfoCompo.RedTransparencyMat;
            }

            for (int i = 0; i < _skinRenderers.Length; i++)
            {
                _skinRenderers[i].material = BuildingInfoCompo.RedTransparencyMat;
            }
        }
        else
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
        
    }

    public void MatChangeToNormal()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshRenderers[i].material = _meshNormalMats[i];
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            _skinRenderers[i].material = _skinNormalMats[i];
        }
    }

    private void OnEnable()
    {
        if (_phaseStartSubscriptionAction != null)
        {
            SignalHub.OnBattlePhaseStartEvent -= _phaseStartSubscriptionAction;
        }
    }

}
