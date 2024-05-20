using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionInventoryUI : PopupUI
{
    private bool _onDisciption = true;

    public void ShowLegionUIPanel()
    {
        UIManager.Instance.ShowPanel("LegionInventory");

        LegionInventoryManager.Instance.CanShowUI(true);

        LegionInventoryManager.Instance.SaveLegion(); //Ȥ�� �𸣴ϱ� ����
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);

        if (_onDisciption)
        {
            UIManager.Instance.GifController.ShowGif(GifType.LegionUI);
            ShowDescript();
        }

    }

    public void HideLegionInventoryUI()
    {
        UIManager.Instance.HidePanel("LegionInventory");

        LegionInventoryManager.Instance.CanShowUI(false);
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
        UIManager.Instance.ShowPanel("Description");
        _onDisciption = false;
    }
}
