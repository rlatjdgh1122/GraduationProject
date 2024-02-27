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
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : WorkableObject
{
    public BuildingInfo BuildingInfoCompo;
    private bool isInstalled = false;
    public bool IsInstalled => isInstalled;

    private Material[] _meshNormalMats;
    private MeshRenderer[] _meshRenderers;

    private Material[] _skinNormalMats;
    private SkinnedMeshRenderer[] _skinRenderers;

    protected override void Awake()
    {
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
        isInstalled = true;
        CancleInsall();
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

    public void CancleInsall()
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

    protected virtual void Update()
    {
        if(isInstalled)
        {
            Running(); // 설치 되면 역할 수행
        }
    }

    protected abstract void Running();
}
