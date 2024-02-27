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
    private TextMeshProUGUI _warningText;
    private CanvasGroup _warningPanel;

    private ResourceObject CurrentResource;

    public override void Awake()
    {
        base.Awake();

        _canvasGroup = GetComponent<CanvasGroup>();
        _resourceNameText = transform.Find("_ResourceName").GetComponent<TextMeshProUGUI>();
        _recieveCountText = transform.Find("_RecieveCount").GetComponent<TextMeshProUGUI>();
        _needWorkerCountText = transform.Find("_NeedWorkerCount").GetComponent<TextMeshProUGUI>();
        _currentWorkerCountText = transform.Find("_CurrentWorkerCount").GetComponent<TextMeshProUGUI>();

        _warningPanel = transform.Find("_WarningPanel").GetComponent<CanvasGroup>(); 
        _warningText = _warningPanel.transform.Find("_WarningText").GetComponent<TextMeshProUGUI>();
        _resourceIcon = transform.Find("_ResourceIcon").GetComponent<Image>();
    }

    public override void EnableUI(float time, object obj)
    {
        base.EnableUI(time, obj);

        CurrentResource = obj as ResourceObject;
        Setting();
        _canvasGroup.DOFade(1, time);
    }

    private void Setting()
    {
        _resourceNameText.text = CurrentResource.ResourceName;
        _resourceIcon.sprite = CurrentResource.ResourceImage;
        _recieveCountText.text = CurrentResource.ReceiveCountWhenCompleted.ToString();
        _needWorkerCountText.text = $"최소 일꾼 {CurrentResource.RequiredWorkerCount}마리 필요";
        _currentWorkerCountText.text = CurrentResource.CurrentWorkerCount.ToString();
    }

    public void IncreaseWorkerCount()
    {
        if (WorkerManager.Instance.WorkerCount < CurrentResource.RequiredWorkerCount)
        {
            ShowWarningText("최소 필요 일꾼이 부족합니다");
        }
        else if (CurrentResource.CurrentWorkerCount < WorkerManager.Instance.WorkerCount)
        {
            CurrentResource.CurrentWorkerCount++;
            _currentWorkerCountText.text = CurrentResource.CurrentWorkerCount.ToString();
        } 
        else
        {
            ShowWarningText("보유중인 일꾼이 부족합니다");
        }
    }

    public void DecreaseWorkerCount()
    {
        if (CurrentResource.CurrentWorkerCount > 0)
        {
            CurrentResource.CurrentWorkerCount--;
            _currentWorkerCountText.text = CurrentResource.CurrentWorkerCount.ToString();
        }
    }

    public void SendWorkers()
    {
        if (CurrentResource.CurrentWorkerCount >= CurrentResource.RequiredWorkerCount)
        {
            WorkerManager.Instance.SendWorkers(CurrentResource.CurrentWorkerCount, CurrentResource);
            ClosePanel();
        }
        else
        {
            ShowWarningText("일꾼이 부족하여 보낼 수 없습니다");
        }
    }

    private void ShowWarningText(string message)
    {
        UIManager.Instance.InitializHudTextSequence();

        _warningText.text = message;

        UIManager.Instance.HudTextSequence.Append(_warningPanel.DOFade(1, 0.08f))
                .AppendInterval(0.8f)
                .Append(_warningPanel.DOFade(0, 0.08f));
    }

    public void ClosePanel()
    {
        CurrentResource.CurrentWorkerCount = 0;
        DisableUI(0.6f, null);
    }

    public override void DisableUI(float time, Action action)
    {
        _canvasGroup.DOFade(0, time);
    }
}
