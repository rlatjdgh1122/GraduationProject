using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvokedFeedback : CombatFeedback
{
    public override bool StartFeedback()
    {
        _prevTarget = owner.CurrentTarget;
    }
    public override bool FinishFeedback()
    {
        return true;
    }


}
