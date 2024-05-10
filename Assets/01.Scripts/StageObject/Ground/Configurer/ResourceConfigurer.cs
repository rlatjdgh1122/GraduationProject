using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceConfigurer : BaseElementsConfigurer
{
    private readonly ResourceGeneratePattern[] _resourceGeneratePattern;

    public ResourceConfigurer(Transform transform, ResourceGeneratePattern[] resourceGeneratePattern) : base(transform)
    {
        _resourceGeneratePattern = resourceGeneratePattern;
    }

    public ResourceObject[] SetResource(List<Vector3> previousElementsPositions)
    {
        int randIdx = Random.Range(0, _resourceGeneratePattern.Length);
        ResourceGeneratePattern spawnResourcePattern = _resourceGeneratePattern[randIdx];

        int spawnResourcesCount = spawnResourcePattern.resourceCounts.Sum();

        ResourceObject[] resources = new ResourceObject[spawnResourcesCount];

        for (int i = 0; i < spawnResourcePattern.resourceTypes.Length; i++)
        {
            ResourceName spawnResourceType = spawnResourcePattern.resourceTypes[i];

            for (int j = 0; j < spawnResourcePattern.resourceCounts[i]; j++)
            {
                ResourceObject spawnResource = PoolManager.Instance.Pop(spawnResourceType.ToString()) as ResourceObject;
                SetPosition(spawnResource.gameObject, transform, previousElementsPositions);
                resources[i] = spawnResource;
            }
        }

        return resources;

        //float resourceCountProportion = 0.5f;
        //int minResourceCount = 1;
        //int maxResourceCount = 3;

        //int resourceCount = GetRandomElementsCount(minResourceCount, maxResourceCount, resourceCountProportion);

        //ResourceObject[] resources = new ResourceObject[resourceCount];

        //for (int i = 0; i < resourceCount; i++)
        //{
        //    int randomIdx = Random.Range(0, _resourceNames.Length);
        //    string resourceName = _resourceNames[randomIdx];
        //    ResourceObject spawnResource = PoolManager.Instance.Pop(resourceName) as ResourceObject;
        //    resources[i] = spawnResource;

        //    SetPosition(spawnResource.gameObject, transform, previousElementsPositions);
        //}

        //return resources;
    }
}
