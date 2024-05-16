using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource
{
    public ResourceDataSO resourceData;
    public int stackSize;

    public Resource(ResourceDataSO resourceData)
    {
        this.resourceData = resourceData;
        AddStack(0);
    }

    public void AddStack(int count)
    {
        stackSize += count;
    }

    public void RemoveStack(int count = 1)
    {
        stackSize -= count;
    }
}
