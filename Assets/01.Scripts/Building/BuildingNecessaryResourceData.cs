using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BuildingNecessaryResourceData
{
    [SerializeField]
    private Resource _necessaryResources;
    public Resource NecessaryResource { get { return _necessaryResources; } }

    [SerializeField]
    private int necessaryResourceCount;
    public int NecessaryResourceCount { get { return necessaryResourceCount; } }
}
