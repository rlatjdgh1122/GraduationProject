using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUIObserver : PopupUI
{
    private Subject _subject = new Subject();
    private ResourceObject resource => ResourceManager.Instance.SelectedResource;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _subject.RegisterReceiver(GetComponentInChildren<IReceiver>());
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        _subject.Notify(resource);
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public void IncreaseWorkerCount()
    {
        if (WorkerManager.Instance.MaxWorkerCount < resource.RequiredWorkerCount)
        {
            ShowAndSettingWarningUI("�ּ� �ʿ� �ϲ��� �����մϴ�");
        }
        else if (resource.CurrentWorkerCount < WorkerManager.Instance.MaxWorkerCount)
        {
            resource.CurrentWorkerCount++;
            _subject.Notify(resource);
        }
        else
        {
            ShowAndSettingWarningUI("�������� �ϲ��� �����մϴ�");
        }
    }

    public void DecreaseWorkerCount()
    {
        if (resource.CurrentWorkerCount > 0)
        {
            resource.CurrentWorkerCount--;
            _subject.Notify(resource);
        }
    }

    public void SendWorkers()
    {
        if (resource.resourceType == ResourceType.Stone && 
            WorkerManager.Instance.AvailiableWorkerCount < resource.CurrentWorkerCount)
        {
            ShowAndSettingWarningUI("�ϲ��� �����Ͽ� ���� �� �����ϴ�.");
            return;
        }
        else if (resource.resourceType == ResourceType.Wood &&
            WorkerManager.Instance.AvailiableWorkerCount < resource.CurrentWorkerCount)
        {
            ShowAndSettingWarningUI("�ϲ��� �����Ͽ� ���� �� �����ϴ�.");
            return;
        }

        if (resource.CurrentWorkerCount >= resource.RequiredWorkerCount)
        {
            WorkerManager.Instance.SendWorkers(resource.CurrentWorkerCount, resource);
            HidePanel();
        }
        else
        {
            ShowAndSettingWarningUI("�ϲ��� �����Ͽ� ���� �� �����ϴ�.");
        }
    }

    private void ShowAndSettingWarningUI(string text)
    {
        UIManager.Instance.ShowWarningUI(text);
    }
}
