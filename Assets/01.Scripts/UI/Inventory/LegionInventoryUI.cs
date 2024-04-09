using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionInventoryUI : PopupUI
{
    public void ShowLegionUIPanel()
    {
        UIManager.Instance.ShowPanel(this.gameObject.name);

        LegionInventoryManager.Instance.SaveLegion(); //Ȥ�� �𸣴ϱ� ����
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public override void HidePanel()
    {
        base.HidePanel();

        if (LegionInventoryManager.Instance.ChangedInCurrentLegion())
        {
            UIManager.Instance.ShowPanel("SaveLegionPanel");
        }
        else
        {
            LegionInventoryManager.Instance.SaveLegion(); //Ȥ�� �𸣴ϱ� ����
            UIManager.Instance.HidePanel(this.gameObject.name);
        }
    }

    public void SaveLegion()
    {
        LegionInventoryManager.Instance.SaveLegion();
        UIManager.Instance.ShowWarningUI("���� ����!");
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
