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

    public bool OnResourceUI { get; set; }
    public bool IsGifFirst { get; set; } = true;

    public bool OnlyGetOneStone { get; set; } = false;
    public bool OnlyGetOneWood { get; set; } = false;

    public override void Awake()
    {
        //resourceStack = new List<Resource>();
        resourceDictionary = new Dictionary<ResourceDataSO, Resource>();
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
}
