using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : PopupUI
{
    private TextMeshProUGUI _resourceNameText;
    private TextMeshProUGUI _recieveCountText;
    private TextMeshProUGUI _needWorkerCountText;
    private TextMeshProUGUI _currentWorkerCountText;
    private Image _resourceIcon;
    private TextMeshProUGUI _warningText;
    private CanvasGroup _warningPanel;

    private ResourceObject CurrentResoruce => ResourceManager.Instance.SelectedResource;

    public override void Awake()
    {
        base.Awake();

        _resourceNameText = transform.Find("_ResourceName").GetComponent<TextMeshProUGUI>();
        _recieveCountText = transform.Find("_RecieveCount").GetComponent<TextMeshProUGUI>();
        _needWorkerCountText = transform.Find("_NeedWorkerCount").GetComponent<TextMeshProUGUI>();
        _currentWorkerCountText = transform.Find("_CurrentWorkerCount").GetComponent<TextMeshProUGUI>();
        _warningPanel = transform.Find("_WarningPanel").GetComponent<CanvasGroup>(); 
        _warningText = _warningPanel.transform.Find("_WarningText").GetComponent<TextMeshProUGUI>();
        _resourceIcon = transform.Find("_ResourceIcon").GetComponent<Image>();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        Setting();
    }

    //public override void EnableUI(float time, object obj)
    //{
    //    base.EnableUI(time, obj);

    //    CurrentResource = obj as ResourceObject;
    //    Setting();
    //    _canvasGroup.DOFade(1, time);
    //}

    private void Setting()
    {
        _resourceNameText.text = CurrentResoruce.ResourceName;
        _resourceIcon.sprite = CurrentResoruce.ResourceImage;
        _recieveCountText.text = CurrentResoruce.ReceiveCountWhenCompleted.ToString();
        _needWorkerCountText.text = $"최소 일꾼 {CurrentResoruce.RequiredWorkerCount}마리 필요";
        _currentWorkerCountText.text = CurrentResoruce.CurrentWorkerCount.ToString();
    }

    public void IncreaseWorkerCount()
    {
        if (WorkerManager.Instance.WorkerCount < CurrentResoruce.RequiredWorkerCount)
        {
            ShowWarningText("최소 필요 일꾼이 부족합니다");
        }
        else if (CurrentResoruce.CurrentWorkerCount < WorkerManager.Instance.WorkerCount)
        {
            CurrentResoruce.CurrentWorkerCount++;
            _currentWorkerCountText.text = CurrentResoruce.CurrentWorkerCount.ToString();
        } 
        else
        {
            ShowWarningText("보유중인 일꾼이 부족합니다");
        }
    }

    public void DecreaseWorkerCount()
    {
        if (CurrentResoruce.CurrentWorkerCount > 0)
        {
            CurrentResoruce.CurrentWorkerCount--;
            _currentWorkerCountText.text = CurrentResoruce.CurrentWorkerCount.ToString();
        }
    }

    public void SendWorkers()
    {
        if (CurrentResoruce.CurrentWorkerCount >= CurrentResoruce.RequiredWorkerCount)
        {
            WorkerManager.Instance.SendWorkers(CurrentResoruce.CurrentWorkerCount, CurrentResoruce);
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
        CurrentResoruce.CurrentWorkerCount = 0;
        HidePanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
