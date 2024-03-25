using DG.Tweening;
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

        _subject.RegisterReceiver(transform.Find("MainUI").GetComponent<IReceiver>());
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
        if (WorkerManager.Instance.WorkerCount < resource.RequiredWorkerCount)
        {
            UIManager.Instance.ShowWarningUI(new Vector3
                (_rectTransform.position.x + 275f, _rectTransform.position.y, _rectTransform.position.z), "�ּ� �ʿ� �ϲ��� �����մϴ�");
        }
        else if (resource.CurrentWorkerCount < WorkerManager.Instance.WorkerCount)
        {
            resource.CurrentWorkerCount++;
            _subject.Notify(resource);
        }
        else
        {
            UIManager.Instance.ShowWarningUI(new Vector3
                (_rectTransform.position.x + 275f, _rectTransform.position.y, _rectTransform.position.z), "�������� �ϲ��� �����մϴ�");
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
        if (resource.CurrentWorkerCount >= resource.RequiredWorkerCount)
        {
            WorkerManager.Instance.SendWorkers(resource.CurrentWorkerCount, resource);
            HidePanel();
        }
        else
        {
            UIManager.Instance.ShowWarningUI(new Vector3
                (_rectTransform.position.x + 275f, _rectTransform.position.y, _rectTransform.position.z), "�ϲ��� �����Ͽ� ���� �� �����ϴ�.");
        }
    }
}
