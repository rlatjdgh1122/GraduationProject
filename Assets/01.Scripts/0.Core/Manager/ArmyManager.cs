using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    //군단의 스탯을 조정하고 움직임도 여기서 설정해줄듯   
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddStat();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RemoveStat();
        }
    }
    public void AddStat()
    {
        var army = ArmySystem.Instace.GetCurArmy();
        foreach (var soldier in army.Soldiers)
        {
            soldier.AddStat(10, StatType.Damage, StatMode.Increase);
        }
    }
    public void RemoveStat()
    {
        var army = ArmySystem.Instace.GetCurArmy();
        foreach (var soldier in army.Soldiers)
        {
            soldier.RemoveStat(10, StatType.Damage, StatMode.Increase);
        }
    }
}
