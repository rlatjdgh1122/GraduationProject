using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance;

    private Dictionary<string, Pool<PoolableMono>> pools =
        new Dictionary<string, Pool<PoolableMono>>();


    private Transform trmParent;
    public PoolManager(Transform trmParent)
    {
        this.trmParent = trmParent;
    }
    public void CreatePool(PoolableMono prefab, int count)
    {
        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, trmParent, count);
        pools.Add(prefab.gameObject.name, pool);

    }
    public PoolableMono Pop(string prefabName)
    {
        if (pools.ContainsKey(prefabName) == false)
        {
            Debug.LogError($"Prefab doesn`t exist on pool list : {prefabName}");
            return null;
        }

        PoolableMono item = pools[prefabName].Pop();
        item.Reset();
        return item;
    }
    public void Push(PoolableMono obj)
    {
        pools[obj.name].Push(obj);
    }
}
