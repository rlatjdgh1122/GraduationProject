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
    public Penguin General; //�屺

    public ArmyInfo info;

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


