using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUpgrade : PopupUI
{
    public GeneralMainUI MainUI;

    public override void Awake()
    {
        base.Awake();
    }

    public void Upgrade()
    {
        MainUI.Upgrade();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ExitButton()
    {
        base.ExitButton();
    }
}
