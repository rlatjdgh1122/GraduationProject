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

        Debug.Log("HI");

        if (LegionInventoryManager.Instance.ChangedInCurrentLegion())
        {
            UIManager.Instance.ShowPanel("SaveLegionPanel");
        }
        else
        {
            LegionInventoryManager.Instance.SaveLegion(); //Ȥ�� �𸣴ϱ� ����
        }
    }

    public void SaveLegion()
    {
        Debug.Log("����");
        LegionInventoryManager.Instance.SaveLegion();
        UIManager.Instance.ShowWarningUI("���� ����!");
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
