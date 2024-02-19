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
    [field: SerializeField]
    public Grid GridCompo;
}

//[RequireComponent(typeof(Health))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Grid))]
public abstract class BaseBuilding : PoolableMono
{
    public BuildingInfo BuildingInfoCompo;

    private bool isInstalled = false;

    private void Awake()
    {
        SetUpCompo();
    }

    private void SetUpCompo()
    {
        BuildingInfoCompo.GridCompo = GetComponent<Grid>();
    }

    public void Installed()
    {
        isInstalled = true;
    }

    //protected virtual void Update()
    //{
    //    if(isInstalled)
    //    {
    //        Running(); // ��ġ �Ǹ� ���� ����
    //    }
    //}

    protected abstract void Running();
}
