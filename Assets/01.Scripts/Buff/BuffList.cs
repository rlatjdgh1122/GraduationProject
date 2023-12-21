using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffList : ScriptableObject
{
    public List<Buff> buffs = new();

    public void SetOnwer(BaseBuilding onwer)
    {
        foreach (var buff in buffs)
            buff.SetOnwer(onwer);
    }
}
