using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class GeneralDetailData
{
    [Header("�屺 �нú� ����")]
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

    //�̰� �屺 ����
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
