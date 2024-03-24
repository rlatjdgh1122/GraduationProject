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
            if (resource.stackSize <= count) //���� ���� �����Ұ����� �� �ڿ��� Stack���� ���ų� �� ���ٸ� List�� Dictionary���� �� �ڿ� �ƿ� ����
            {
                resourceStack.Remove(resource);
                resourceDictionary.Remove(resourceData);
            }
            else //�װ� �ƴ϶�� �׳� Stack�� count��ŭ ���ҽ�Ŵ
            {
                resource.RemoveStack(count);
            }
        }
    }
}
