using System.Collections.Generic;
using System.Diagnostics;

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
    public int Legion; //���° ����
    public bool IsMoving; //�����̴� ���ΰ�
    public List<Penguin> Soldiers = new(); //���� ��ϵ�
    public General General; //�屺

    public ArmyInfo info;

    public void AddStat(LigeonStatAdjustment ligeonStat)
    {
        var IncStatList = ligeonStat.IncStat;
        var DecStatList = ligeonStat.DecStat;

        foreach (var incStat in IncStatList)
        {
            AddStat(incStat.value, incStat.type, StatMode.Increase);
        }

        foreach (var DecStat in DecStatList)
        {
            AddStat(DecStat.value, DecStat.type, StatMode.Decrease);
        }
    }
    public void AddStat(int value, StatType type, StatMode mode)
    {
        Debug.WriteLine(type);

        General?.AddStat(value, type, mode);

        foreach (var solider in Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        General?.RemoveStat(value, type, mode);

        foreach (var solider in Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
}


