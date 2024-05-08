using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConfigurer : BaseConfigurer
{
    private readonly string[] _resourceNames;

    public ResourceConfigurer(Transform transform, string[] resourceNames) : base(transform)
    {
        _resourceNames = resourceNames;
    }

    public ResourceObject[] SetResource(List<Vector3> previousElementsPositions)
    {
        float resourceCountProportion = 0.5f;
        int minResourceCount = 1;
        int maxResourceCount = 3;

        int resourceCount = GetRandomElementsCount(minResourceCount, maxResourceCount, resourceCountProportion);

        ResourceObject[] resources = new ResourceObject[resourceCount];

        for (int i = 0; i < resourceCount; i++)
        {
            int randomIdx = Random.Range(0, _resourceNames.Length);
            string resourceName = _resourceNames[randomIdx];
            ResourceObject spawnResource = PoolManager.Instance.Pop(resourceName) as ResourceObject;
            resources[i] = spawnResource;

            SetPosition(spawnResource.gameObject, transform, previousElementsPositions);
        }

        return resources;
    }
}
