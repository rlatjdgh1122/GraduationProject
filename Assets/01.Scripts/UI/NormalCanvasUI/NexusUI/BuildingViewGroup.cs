using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingViewGroup : PopupUI
{
    public BuildingType Category;
    public bool IsArrived;

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }
}
