using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 시너지 건물의 타입
/// </summary>
public enum SynergyBuildingType
{
    None = 0,
    KatanaSynergyBuilding,
    PoliceSynergyBuilding,
    LanceSynergyBuilding
}

/// <summary>
/// 시너지 건물의 업그레이드 관련 정보
/// </summary>
[Serializable]
public struct SynergyUpgradeInfo
{
    [TextArea] 
    public string UpgradeDescription;

    //스텟 관련 뭐가 들어가면 될듯?
    //?????
}

/// <summary>
/// 시너지 건물의 정보 데이터
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