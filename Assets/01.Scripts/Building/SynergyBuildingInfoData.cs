using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ó��� �ǹ��� Ÿ��
/// </summary>
public enum SynergyBuildingType
{
    None = 0,
    KatanaSynergyBuilding,
    PoliceSynergyBuilding,
    LanceSynergyBuilding
}

/// <summary>
/// �ó��� �ǹ��� ���׷��̵� ���� ����
/// </summary>
[Serializable]
public struct SynergyUpgradeInfo
{
    [TextArea] 
    public string UpgradeDescription;

    //���� ���� ���� ���� �ɵ�?
    //?????
}

/// <summary>
/// �ó��� �ǹ��� ���� ������
/// </summary>
[Serializable]
public class SynergyBuildingInfoData
{
    [SerializeField]
    private SynergyBuildingType _synergyBuildingType = SynergyBuildingType.None;

    [SerializeField]
    private List<SynergyUpgradeInfo> _synergyUpgradeInfo = new List<SynergyUpgradeInfo>();

    public SynergyBuildingType SynergyBuildingType => _synergyBuildingType;
    public List<SynergyUpgradeInfo> SynergyUpgradeInfoList => _synergyUpgradeInfo;
}