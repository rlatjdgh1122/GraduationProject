using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionInventoryUI : PopupUI
{
    public List<DescriptionUI> Descriptions;
    private bool _isDescript = false;

    public void ShowLegionUIPanel()
    {
        UIManager.Instance.ShowPanel("LegionInventory");

        LegionInventoryManager.Instance.SaveLegion(); //Ȥ�� �𸣴ϱ� ����
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void HideLegionInventoryUI()
    {
        UIManager.Instance.HidePanel("LegionInventory");
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
        }
    }

    public void SaveLegion()
    {
        LegionInventoryManager.Instance.SaveLegion();
        UIManager.Instance.ShowWarningUI("���� ����!");
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void UndoLegion()
    {
        UIManager.Instance.ShowWarningUI("���� ���");
        if (!LegionInventoryManager.Instance.ChangedInCurrentLegion()) return;

        LegionInventoryManager.Instance.UndoLegion();
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void ShowDescript()
    {
        _isDescript = !_isDescript;

        foreach (var ui in Descriptions)
        {
            ui.ShowAllDescript(_isDescript);
        }
    }
}
