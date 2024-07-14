using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidPanel : PopupUI
{
    public void ShowGuidPanel()
    {
        UIManager.Instance.ShowPanel("GuidePanel");
    }

    public void HideGuidPanel()
    {
        UIManager.Instance.HidePanel("GuidePanel");
    }
}