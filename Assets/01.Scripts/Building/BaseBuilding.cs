using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector]
    public Material NormalMat;
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : PoolableMono
{
    private MeshRenderer _meshRenderer;
    public BuildingInfo BuildingInfoCompo;
    private bool isInstalled = false;
    public bool IsInstalled => isInstalled;

    private void Awake()
    {
        SetUpCompo();
    }

    private void SetUpCompo()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        BuildingInfoCompo.NormalMat = _meshRenderer.material;
        BuildingInfoCompo.GridCompo = GetComponent<Grid>();
    }

    public void Installed()
    {
        isInstalled = true;
        CancleInsall();
    }

    public void SetSelect()
    {
        _meshRenderer.material = BuildingInfoCompo.TransparencyMat;
    }

    public void CancleInsall()
    {
        _meshRenderer.material = BuildingInfoCompo.NormalMat;
    }

    //protected virtual void Update()
    //{
    //    if(isInstalled)
    //    {
    //        Running(); // 설치 되면 역할 수행
    //    }
    //}

    protected abstract void Running();
}
