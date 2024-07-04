using System;
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

    public List<ResourceDataSO> list = new();

    public override void Awake()
    {
        //resourceStack = new List<Resource>();
        resourceDictionary = new Dictionary<ResourceDataSO, Resource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddResource(list[0], 5);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddResource(list[1], 5);
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

    public void RemoveAllResources(int count = 1, Action onSuccesAction = null) //���� ������ ��ġ���� ������ ����̴�. ���߿� ��ġ���� ����.
    {
        if (resourceStack[0].stackSize < count || resourceStack[1].stackSize < count)
        {
            UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�.");
            return;
        }

        if (resourceStack[0].stackSize <= count)
        {
            resourceStack.Remove(resourceStack[0]);
            resourceDictionary.Remove(resourceStack[0].resourceData);
            OnUIUpdate(null, 0);
        }
        else
        {
            resourceStack[0].RemoveStack(count);
            OnUIUpdate(resourceStack[0], count);
        }

        if (resourceStack[1].stackSize <= count)
        {
            resourceStack.Remove(resourceStack[1]);
            resourceDictionary.Remove(resourceStack[1].resourceData);
            OnUIUpdate(null, 0);
        }
        else
        {
            resourceStack[1].RemoveStack(count);
            OnUIUpdate(resourceStack[1], count);
        }

        onSuccesAction?.Invoke();
    }
}
