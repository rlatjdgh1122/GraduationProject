using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class GeneralDetailData
{
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
    public GeneralDetailData GeneralData;

    public int Level
    {
        get
        {
            return GeneralData.level;
        }
        set
        {
            GeneralData.level = value;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        GeneralData.levelUpPrice.AddIncrease(10); //일단 임시 UI먼저함
    }

    //public void UpdateAblitiyUI(TextMeshProUGUI name, Slider atk, Slider def, Slider rng)
    //{
    //    name.text = PenguinName;
    //    def.value = PenguinData.hp;
    //    atk.value = PenguinData.atk;
    //    rng.value = PenguinData.range;
    //}
}
