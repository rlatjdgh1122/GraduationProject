using System.Collections.Generic;
using UnityEngine;

public class PenguinInfoUI : PopupUI
{
    [SerializeField]
    private GameObject _statItemObject = null;

    //펭귄매니저에서 더미를 클릭할때 넘겼던 스탯을 현재스탯에서 받아주고 그걸 받아주기
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
