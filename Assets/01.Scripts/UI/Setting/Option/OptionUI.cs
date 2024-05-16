using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : PopupUI
{
    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public void HideOptionUI()
    {
        UIManager.Instance.HidePanel("OptionUI");
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
