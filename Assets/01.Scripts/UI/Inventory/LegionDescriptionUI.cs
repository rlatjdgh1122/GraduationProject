using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionDescriptionUI : PopupUI
{
    public void HideLegionDescriptionUI()
    {
        UIManager.Instance.HidePanel("Description");
    }
}