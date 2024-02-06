using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct BuildingInfo
{
    [HideInInspector]
    public MeshRenderer MeshRendererCompo;
    [HideInInspector]
    public Material NormalMaterial;
    public Material TransparencyMaterial;
    [HideInInspector]
    public Grid Grid;
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : PoolableMono
{
    public BuildingInfo BuildingInfoCompo;

    private bool isSelected = false;
    private bool isPlaced = false;

    private void Awake()
    {
        SetUpCompo();
    }

    private void SetUpCompo()
    {
        BuildingInfoCompo.MeshRendererCompo = GetComponent<MeshRenderer>();
        BuildingInfoCompo.NormalMaterial = BuildingInfoCompo.MeshRendererCompo.material;
        BuildingInfoCompo.Grid = GetComponent<Grid>();
    }

    public void SetSelected()
    {
        BuildingInfoCompo.MeshRendererCompo.material = BuildingInfoCompo.TransparencyMaterial; // 선택되어 투명메테리얼로 바꿈
        isSelected = true;
    }

    public void Deselect()
    {
        isSelected = false;
        BuildingInfoCompo.MeshRendererCompo.material = BuildingInfoCompo.NormalMaterial; // 선택되어 원래메테리얼로 바꿈
    }

    public void Placed()
    {
        isPlaced = true;
        Deselect();
    }

    protected virtual void Update()
    {
        if(isPlaced)
        {
            Running(); // 설치 되면 역할 수행
        }
    }

    protected abstract void Running();
}
