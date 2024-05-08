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
            ShowAndSettingWarningUI("최소 필요 일꾼이 부족합니다");
        }
        else if (resource.CurrentWorkerCount < WorkerManager.Instance.MaxWorkerCount)
        {
            resource.CurrentWorkerCount++;
            _subject.Notify(resource);
        }
        else
        {
            ShowAndSettingWarningUI("보유중인 일꾼이 부족합니다");
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
            ShowAndSettingWarningUI("일꾼이 부족하여 보낼 수 없습니다.");
            return;
        }
        else if (resource.resourceType == ResourceType.Wood &&
            WorkerManager.Instance.AvailiableWorkerCount < resource.CurrentWorkerCount)
        {
            ShowAndSettingWarningUI("일꾼이 부족하여 보낼 수 없습니다.");
            return;
        }

        if (resource.CurrentWorkerCount >= resource.RequiredWorkerCount)
        {
            WorkerManager.Instance.SendWorkers(resource.CurrentWorkerCount, resource);
            HidePanel();
        }
        else
        {
            ShowAndSettingWarningUI("일꾼이 부족하여 보낼 수 없습니다.");
        }
    }

    private void ShowAndSettingWarningUI(string text)
    {
        UIManager.Instance.ShowWarningUI(text);
    }
}
