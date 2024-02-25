using System.Collections.Generic;


[System.Serializable]
public struct ArmyInfo //UI부분, 기획이 더 필요
{
    //인원수
    public int totalCount;
    public int basicCount;
    public int archerCount;
    public int shieldCount;

    //스탯이 몇배정도 증가하였는가
    public float basicTimes;
    public float archerTimes;
    public float shieldTimes;
}

[System.Serializable]
public class Army
{
    public int Legion; //몇번째 군단
    public bool IsMoving; //움직이는 중인가
    public List<Penguin> Soldiers = new(); //군인 펭귄들
    public Penguin General; //장군

    public ArmyInfo info;

    public void AddStat(int value, StatType type, StatMode mode)
    {
        foreach (var solider in Soldiers)
        {
            solider.AddStat(value, type, mode);
        }
    }
    public void RemoveStat(int value, StatType type, StatMode mode)
    {
        foreach (var solider in Soldiers)
        {
            solider.RemoveStat(value, type, mode);
        }
    }
}


