using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public struct ArmyInfo //UI�κ�, ��ȹ�� �� �ʿ�
{
    //�ο���
    public int totalCount;
    public int basicCount;
    public int archerCount;
    public int shieldCount;

    //������ ������� �����Ͽ��°�
    public float basicTimes;
    public float archerTimes;
    public float shieldTimes;
}

[System.Serializable]
public class Army
{
    public string LegionName; //���° ����
    public bool IsCanReadyAttackInCurArmySoldiersList = true; //���� ��ü�� ������ �غ� �Ǿ��°�

    public ArmyFollowCam FollowCam = null; //���� ������Ʈ
    public ArmyInfo Info; //����

    public List<Penguin> Soldiers = new(); //���� ��ϵ�
    public General General = null; //�屺

    public void AddStat(int value, StatType type, StatMode mode)
    {
        this.General?.AddStat(value, type, mode);
        foreach (var solider in this.Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }

    public void AddStat(List<Ability> abilities)
    {
        foreach (var incStat in abilities)
        {
            AddStat(incStat.value, incStat.statType, incStat.statMode);
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        this.General?.RemoveStat(value, type, mode);

        foreach (var solider in this.Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
}
public class ArmyFollowCam
{
    public GameObject Obj = null;
    public Vector3 mousePos = Vector3.zero;
    public float moveSpeed = 5f;
    public bool isInGeneral = false;
}


