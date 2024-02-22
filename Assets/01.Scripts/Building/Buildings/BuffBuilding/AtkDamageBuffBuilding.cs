using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkDamageBuffBuilding : BuffBuilding
{
    protected override void Running()
    {
        base.Running();
    }

    protected override void BuffEvent(float value)
    {
        throw new System.NotImplementedException();
    }

    protected override float SetBuffValue(float value)
    {
        this.buffValue = value;
        return this.buffValue;
    }

    protected override float GetBuffValue()
    {
        return this.SetBuffValue(this.buffValue);
    }
}
