using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyManager : MonoBehaviour
{
    //������ ������ �����ϰ� �����ӵ� ���⼭ �������ٵ�   
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
        var army = ArmySystem.Instance.GetCurArmy();
        army.AddStat(10, StatType.Damage, StatMode.Increase);
    }
    public void RemoveStat()
    {
        var army = ArmySystem.Instance.GetCurArmy();
        army.RemoveStat(10, StatType.Damage, StatMode.Increase);
    }
}
