using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public List<Resource> resourceStack;
    public Dictionary<ResourceDataSO, Resource> resourceDictionary;

    public ResourceObject SelectedResource;

    public override void Awake()
    {
        resourceStack = new List<Resource>();
        resourceDictionary = new Dictionary<ResourceDataSO, Resource>();
    }

    public void AddResource(ResourceDataSO resourceData, int count = 1)
    {
        if (resourceDictionary.TryGetValue(resourceData, out Resource resource))
        {
            resource.AddStack(count);
        }
        else
        {
            Resource newResource = new Resource(resourceData);
            resourceStack.Add(newResource);
            resourceDictionary.Add(resourceData, newResource);
        }
    }

    public void RemoveResource(ResourceDataSO resourceData, int count = 1)
    {
        if (resourceDictionary.TryGetValue(resourceData, out Resource resource))
        {
            if (resource.stackSize <= count) //만약 들어온 삭제할값보다 그 자원의 Stack값이 같거나 더 적다면 List와 Dictionary에서 그 자원 아예 삭제
            {
                resourceStack.Remove(resource);
                resourceDictionary.Remove(resourceData);
            }
            else //그게 아니라면 그냥 Stack값 count만큼 감소시킴
            {
                resource.RemoveStack(count);
            }
        }
    }
}
