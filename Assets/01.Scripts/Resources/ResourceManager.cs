using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct NeedResource
{
    public ResourceType Type;
    public int Count;
}

public class ResourceManager : Singleton<ResourceManager>
{
    public List<Resource> resourceStack;
    public Dictionary<ResourceDataSO, Resource> resourceDictionary;

    public ResourceObject SelectedResource;

    public delegate void OnUIUpdateHandler(Resource resource, int stackCount);
    public event OnUIUpdateHandler OnUIUpdate;

    [SerializeField] private List<ResourceDataSO> _typeByResourceDataList = new();

    

    public override void Awake()
    {
        resourceStack = new List<Resource>();
        resourceDictionary = new Dictionary<ResourceDataSO, Resource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddResource(_typeByResourceDataList[0], 5);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddResource(_typeByResourceDataList[1], 5);
        }
    }

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

    public ResourceDataSO TypeToResourceDataSO(ResourceType type)
    {
        return _typeByResourceDataList.Find(data => data.resourceType == type);
    }

    public bool CheckAllResources(NeedResource[] resourceDataArray)
    {
        for (int i = 0; i < resourceDataArray.Length; i++)
        {
            var data = TypeToResourceDataSO(resourceDataArray[i].Type);
            resourceDictionary.TryGetValue(data, out var saveResource);

            if (saveResource == null || saveResource.stackSize < resourceDataArray[i].Count)
            {
                return false; // 조건을 만족하지 않는 경우 false 반환
            }
        }
        return true; // 모든 조건을 만족하는 경우 true 반환
    }


    public void RemoveResource(NeedResource[] resourceDataArray, Action onSuccesAction = null, Action onFailAction = null)
    {
        foreach (var resourceData in resourceDataArray)
        {
            var data = TypeToResourceDataSO(resourceData.Type);
            resourceDictionary.TryGetValue(data, out var saveResource);

            if (saveResource == null || saveResource.stackSize < resourceData.Count)
            {
                onFailAction?.Invoke();
                return;
            }
        }

        // 모든 자원이 충분히 있다면 자원 제거 작업 수행
        foreach (var resourceData in resourceDataArray)
        {
            var data = TypeToResourceDataSO(resourceData.Type);
            RemoveResource(data, resourceData.Count);
        }

        onSuccesAction?.Invoke();
    }

}
