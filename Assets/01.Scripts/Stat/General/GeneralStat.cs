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
    public GeneralDetailData GeneralPassvieData;
    public GeneralInfoDataSO InfoData;

    public int Level
    {
        get
        {
            return GeneralPassvieData.level;
        }
        set
        {
            GeneralPassvieData.level = value;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        GeneralPassvieData.levelUpPrice.AddIncrease(10); //�ϴ� �ӽ� UI������
    }

    //public void UpdateAblitiyUI(TextMeshProUGUI name, Slider atk, Slider def, Slider rng)
    //{
    //    name.text = PenguinName;
    //    def.value = PenguinData.hp;
    //    atk.value = PenguinData.atk;
    //    rng.value = PenguinData.range;
    //}
}
