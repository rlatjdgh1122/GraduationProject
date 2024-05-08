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

    tree_medium_type01,
    tree_small_type01
}

[Serializable]
public struct ResourceGeneratePattern
{
    public ResourceName[] resourceTypes;
    public int[] resourceCounts;
}
