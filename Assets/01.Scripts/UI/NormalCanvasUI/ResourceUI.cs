using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : NormalUI
{
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _resourceNameText;
    private TextMeshProUGUI _recieveCountText;
    private TextMeshProUGUI _needWorkerCountText;
    private TextMeshProUGUI _currentWorkerCountText;
    private Image _resourceIcon;

    private ResourceObject _CurrentResource;

    public override void Awake()
    {
        base.Awake();

        _canvasGroup = GetComponent<CanvasGroup>();
        _resourceNameText = transform.Find("_ResourceName").GetComponent<TextMeshProUGUI>();
        _recieveCountText = transform.Find("_RecieveCount").GetComponent<TextMeshProUGUI>();
        _needWorkerCountText = transform.Find("_NeedWorkerCount").GetComponent<TextMeshProUGUI>();
        _currentWorkerCountText = transform.Find("_CurrentWorkerCount").GetComponent<TextMeshProUGUI>();
        _resourceIcon = transform.Find("_ResourceIcon").GetComponent<Image>();
    }

    public override void EnableUI(float time, object obj)
    {
        base.EnableUI(time, obj);

        _CurrentResource = obj as ResourceObject;
        Setting();
        _canvasGroup.DOFade(1, time);
    }

    private void Setting()
    {
        _resourceNameText.text = _CurrentResource.ResourceName;
        _resourceIcon.sprite = _CurrentResource.ResourceImage;
        _recieveCountText.text = _CurrentResource.ReceiveCountWhenCompleted.ToString();
        _needWorkerCountText.text = $"�ּ� �ϲ� {_CurrentResource.NeedWorkerCount}���� �ʿ�";
        _currentWorkerCountText.text = _CurrentResource.CurrentWorkerCount.ToString();
    }

    public void IncreaseWorkerCount()
    {
        _CurrentResource.CurrentWorkerCount++;
        _currentWorkerCountText.text = _CurrentResource.CurrentWorkerCount.ToString();
    }

    public void SendWorkers()
    {
        //������ ����
        WorkerManager.Instance.SendWorkers(_CurrentResource.CurrentWorkerCount, _CurrentResource);
        ClosePanel();
    }

    public void ClosePanel()
    {
        DisableUI(1, null);
    }

    public override void DisableUI(float time, Action action)
    {
        _canvasGroup.DOFade(0, time);
    }
}
