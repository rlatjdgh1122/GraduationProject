using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class GeneralDetailData
{
    [Header("장군 패시브 관련")]
    public bool IsAvailable;
    public int level;
    public Stat levelUpPrice;
    public int levelUpIncreaseValue;
    public List<Ability> abilities;
    //public string passive;
    public Synergy synergy;

    public void LevelUp()
    {
        levelUpPrice.AddIncrease(levelUpIncreaseValue);
    }
}

[CreateAssetMenu(menuName = "SO/Stat/Penguin/General")]
public class GeneralStat : PenguinStat
{
    public GeneralDetailData GeneralDetailData;
    public GeneralInfoDataSO InfoData;

    //이건 장군 레벨
    public int Level
    {
        get
        {
            return GeneralDetailData.level;
        }
        set
        {
            GeneralDetailData.level = value;
            LevelUpPrice();
        }
        
    }

    public void LevelUpPrice()
    {
        GeneralDetailData.LevelUp();
    }
}
