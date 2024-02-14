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
    private Image _resourceIcon;

    private ResourceObject _resource;

    public override void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _resourceNameText = transform.Find("_ResourceName").GetComponent<TextMeshProUGUI>();
        _recieveCountText = transform.Find("_RecieveCount").GetComponent<TextMeshProUGUI>();
        _resourceIcon = transform.Find("_ResourceIcon").GetComponent<Image>();
    }

    public override void EnableUI(float time, object obj)
    {
        _resource = obj as ResourceObject;
        Setting();
        _canvasGroup.DOFade(1, time);
    }

    private void Setting()
    {
        _resourceNameText.text = _resource.ResourceName;
        _resourceIcon.sprite = _resource.ResourceImage;
        _recieveCountText.text = _resource.ReceiveCountWhenCompleted.ToString();
    }

    public void Disable()
    {
        DisableUI(1, null);
    }

    public override void DisableUI(float time, Action action)
    {
        _canvasGroup.DOFade(0, time);
    }
}
