using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceName
{
    stone_large_type01,
    stone_medium_type01,
    stone_small_type01,
    stone_small_type02,

    tree_normal_type01,
    tree_normal_type02
}

[Serializable]
public struct ResourceGeneratePattern
{
    public ResourceName[] resourceTypes;
    public int[] resourceCounts;
}
