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
    public int ID { get; private set; }
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

    [SerializeField]
    private TextMeshProUGUI _installedFinText;

    private BuildingItemInfo _buildingItemInfo;
    public BuildingItemInfo BuildingItemInfoCompo => _buildingItemInfo;

    public BuildingInfo BuildingInfoCompo;

    private bool isInstalled = false;
    public bool IsInstalled => isInstalled;

    public bool Installing { get; set; }

    private Material[][] _meshNormalMats;
    protected MeshRenderer[] _meshRenderers;

    private Material[][] _skinNormalMats;
    protected SkinnedMeshRenderer[] _skinRenderers;

    private bool isInstalling = false;
    public bool IsInstalling => isInstalling;

    private bool isSelected;
    public bool IsSelected => isSelected;

    protected LayerMask _groundLayer = 1 << 3;

    private BattlePhaseStartEvent _phaseStartSubscriptionAction;
    public Ground InstalledGround()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            return hit.collider.transform.parent.GetComponent<Ground>();
        }
        return null;
    }

    public Action OnInstalledEvent = null;

    public override void Init()
    {
        base.Init();

        isInstalled = false;
    }

    protected override void Awake()
    {
        base.Awake();

        SetUpCompo();
    }

    private void SetUpCompo()
    {
        _meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        _skinRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

        _meshNormalMats = new Material[_meshRenderers.Length][];
        _skinNormalMats = new Material[_skinRenderers.Length][];

        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            _meshNormalMats[i] = _meshRenderers[i].materials;
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            _skinNormalMats[i] = _skinRenderers[i].materials;
        }

        BuildingInfoCompo.GridCompo = GetComponent<Grid>();
    }

    public void Installed()
    {
        if (MaxNoiseValue > 0)
        {
            ChangeToTransparencyMat(OutlineColorType.None);
            WorkerManager.Instance.SendBuilders(_buildingItemInfo.NecessaryWokerCount, this);

            _phaseStartSubscriptionAction = () => WorkerManager.Instance.ReturnBuilders(this);
            _phaseStartSubscriptionAction = () => BuildingEnable(isInstalled);
            SignalHub.OnBattlePhaseStartEvent += _phaseStartSubscriptionAction;
            SignalHub.OnBattlePhaseEndEvent += PlusInstalledTime;

            NoiseManager.Instance.AddNoise(MaxNoiseValue);

            OnNoiseExcessEvent += InstallEnd;
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
        if (CurrentNoiseValue > MaxNoiseValue)
        {
            SignalHub.OnBattlePhaseEndEvent -= PlusInstalledTime;
        }
        else
        {
            WorkerManager.Instance.SendBuilders(_buildingItemInfo.NecessaryWokerCount, this);
            BuildingEnable(true);
        }
    }

    public void InstallEnd()
    {
        OnNoiseExcessEvent -= InstallEnd;
        WorkerManager.Instance.ReturnBuilders(this);

        SetInstalled();
    }

    public virtual void SetSelect(BuildingItemInfo info)
    {
        _buildingItemInfo = info;
        isSelected = true;
    }

    protected virtual void SetInstalled()
    {
        MatChangeToNormal();

        Install(true);
        Installing = true;
        isInstalling = false;

        BuildingEnable(true);
        OnInstalled();
        OnInstalledEvent?.Invoke();

        if (_buildingItemInfo != null)
        {
            //_installedFinText.SetText($"{_buildingItemInfo.Name: 설치 완료!}");
            UIManager.Instance.ShowWarningUI($"{_buildingItemInfo.Name: 설치 완료!}");
            //UIManager.Instance.SpawnHudText(_installedFinText);

            //SignalHub.OnBattlePhaseStartEvent -= _phaseStartSubscriptionAction;
        }
    }

    protected virtual void OnInstalled() { }

    protected override void Update()
    {
        if (isInstalled)
        {
            Running(); //   ġ  Ǹ           
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
                Material[] mats = _meshRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.GreenTransparencyMat;
                }
                _meshRenderers[i].materials = mats;
            }

            for (int i = 0; i < _skinRenderers.Length; i++)
            {
                Material[] mats = _skinRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.GreenTransparencyMat;
                }
                _skinRenderers[i].materials = mats;
            }
        }
        else if (colorType == OutlineColorType.Red)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                Material[] mats = _meshRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.RedTransparencyMat;
                }
                _meshRenderers[i].materials = mats;
            }

            for (int i = 0; i < _skinRenderers.Length; i++)
            {
                Material[] mats = _skinRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.RedTransparencyMat;
                }
                _skinRenderers[i].materials = mats;
            }
        }
        else
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                Material[] mats = _meshRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.TransparencyMat;
                }
                _meshRenderers[i].materials = mats;
            }

            for (int i = 0; i < _skinRenderers.Length; i++)
            {
                Material[] mats = _skinRenderers[i].materials;
                for (int j = 0; j < mats.Length; j++)
                {
                    mats[j] = BuildingInfoCompo.TransparencyMat;
                }
                _skinRenderers[i].materials = mats;
            }
        }
    }

    public void MatChangeToNormal()
    {
        for (int i = 0; i < _meshRenderers.Length; i++)
        {
            Material[] mats = _meshNormalMats[i];
            _meshRenderers[i].materials = mats;
        }

        for (int i = 0; i < _skinRenderers.Length; i++)
        {
            Material[] mats = _skinNormalMats[i];
            _skinRenderers[i].materials = mats;
        }
    }

    public void BuildingEnable(bool value)
    {
        //ColliderCompo.enabled = value;

        if (ColliderCompo != null)
            ColliderCompo.enabled = value;
    }

    public void Install(bool value)
    {
        isInstalled = value;
    }

    public virtual void OnDisable()
    {
        if (_phaseStartSubscriptionAction != null)
        {
            SignalHub.OnBattlePhaseStartEvent -= _phaseStartSubscriptionAction;
        }
        Install(false);
    }
}