using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBuff : BaseBuff
{
    public override void SetOnwer(BaseBuilding onwer)
    {
        _onwer = onwer;
    }
}
