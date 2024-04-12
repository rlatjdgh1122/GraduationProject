using System.Collections.Generic;
using UnityEngine;

public class PenguinInfoUI : PopupUI
{
    [SerializeField]
    private GameObject _statItemObject = null;

    //��ϸŴ������� ���̸� Ŭ���Ҷ� �Ѱ�� ������ ���罺�ȿ��� �޾��ְ� �װ� �޾��ֱ�
    private BaseStat _ownerStat => PenguinManager.Instance.GetCurrentStat;
    private List<IStatable> statList = new();


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    private void AddAttackStatItem(Stat stat)
    {
        StatItem statItem = new StatItem(_statItemObject, this.transform);
        string statName = _ownerStat.GetStatNameByStat(stat);

        statItem.Modify(stat, statName);
        statList.Add(statItem);
    }
}
