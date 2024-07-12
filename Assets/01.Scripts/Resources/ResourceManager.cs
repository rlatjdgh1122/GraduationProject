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
            if (resource.stackSize <= count) //���� ���� �����Ұ����� �� �ڿ��� Stack���� ���ų� �� ���ٸ� List�� Dictionary���� �� �ڿ� �ƿ� ����
            {
                resourceStack.Remove(resource);
                resourceDictionary.Remove(resourceData);
                OnUIUpdate(null, 0);
            }
            else //�װ� �ƴ϶�� �׳� Stack�� count��ŭ ���ҽ�Ŵ
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
                return false; // ������ �������� �ʴ� ��� false ��ȯ
            }
        }
        return true; // ��� ������ �����ϴ� ��� true ��ȯ
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

        // ��� �ڿ��� ����� �ִٸ� �ڿ� ���� �۾� ����
        foreach (var resourceData in resourceDataArray)
        {
            var data = TypeToResourceDataSO(resourceData.Type);
            RemoveResource(data, resourceData.Count);
        }

        onSuccesAction?.Invoke();
    }

}
