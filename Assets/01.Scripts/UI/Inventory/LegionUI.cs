using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionUI : PopupUI
{
    public void ShowLegionUIPanel()
    {
        UIManager.Instance.ShowPanel(this.gameObject.name);

        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }

    public void HideLegionUIPanel()
    {
        if(LegionInventoryManager.Instance.ChangedInCurrentLegion())
        {
            UIManager.Instance.ShowPanel("SaveLegionPanel");
        }
        else
        {
            LegionInventoryManager.Instance.SaveLegion(); //혹시 모르니깐 저장
            UIManager.Instance.HidePanel(this.gameObject.name);
        }
    }

    public void SaveLegion()
    {
        LegionInventoryManager.Instance.SaveLegion();
        UIManager.Instance.ShowWarningUI("저장 성공!");
        LegionInventoryManager.Instance.ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
    }
}
