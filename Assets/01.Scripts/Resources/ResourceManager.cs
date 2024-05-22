using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public List<Resource> resourceStack;
    public Dictionary<ResourceDataSO, Resource> resourceDictionary;

    public ResourceObject SelectedResource;

    public delegate void OnUIUpdateHandler(Resource resource, int stackCount);
    public event OnUIUpdateHandler OnUIUpdate;

    public bool IsFirst { get; set; } = true;

    public override void Awake()
    {
        //resourceStack = new List<Resource>();
        resourceDictionary = new Dictionary<ResourceDataSO, Resource>();
    }

    public ResourceDataSO[] resourceArray;

    public void AddResource(ResourceDataSO resourceData, int count)
    {
        if (resourceDictionary.TryGetValue(resourceData, out Resource resource))
        {
            resource.AddStack(count);
            OnUIUpdate(resource, count);
        }
        else
        {
            Resource newResource = new Resource(resourceData);
            resourceStack.Add(newResource);
            resourceDictionary.Add(resourceData, newResource);
            OnUIUpdate(newResource, count);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            AddResource(resourceArray[0], 10);
        if (Input.GetKeyDown(KeyCode.B))
            AddResource(resourceArray[1], 10);
    }

    public void RemoveResource(ResourceDataSO resourceData, int count = 1)
    {
        if (resourceDictionary.TryGetValue(resourceData, out Resource resource))
        {
            if (resource.stackSize <= count) //만약 들어온 삭제할값보다 그 자원의 Stack값이 같거나 더 적다면 List와 Dictionary에서 그 자원 아예 삭제
            {
                resourceStack.Remove(resource);
                resourceDictionary.Remove(resourceData);
                OnUIUpdate(null, 0);
            }
            else //그게 아니라면 그냥 Stack값 count만큼 감소시킴
            {
                resource.RemoveStack(count);
                OnUIUpdate(resource, count);
            }
        }
    }
}
