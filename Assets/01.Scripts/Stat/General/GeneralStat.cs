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
    public string passive;
    public Synergy synergy;
    public List<Ability> abilities;
}

[CreateAssetMenu(menuName = "SO/Stat/Penguin/General")]
public class GeneralStat : PenguinStat
{
    public GeneralDetailData GeneralDetailData;
    public GeneralInfoDataSO InfoData;

    public int Level
    {
        get
        {
            return GeneralDetailData.level;
        }
        set
        {
            GeneralDetailData.level = value;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        GeneralDetailData.levelUpPrice.AddIncrease(10); //�ϴ� �ӽ� UI������
    }
}
