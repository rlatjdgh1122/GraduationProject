using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionInventoryUI : PopupUI
{
    public void ShowLegionUIPanel()
    {
        UIManager.Instance.ShowPanel(this.gameObject.name);

        LegionInventoryManager.Instance.SaveLegion(); //혹시 모르니깐 저장
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
            LegionInventoryManager.Instance.SaveLegion(); //혹시 모르니깐 저장
        }
    }

    public void SaveLegion()
    {
        LegionInventoryManager.Instance.SaveLegion();
        UIManager.Instance.ShowWarningUI("저장 성공!");
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void UndoLegion()
    {
        UIManager.Instance.ShowWarningUI("저장 취소");
        if (!LegionInventoryManager.Instance.ChangedInCurrentLegion()) return;

        LegionInventoryManager.Instance.UndoLegion();
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
