using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class NexusPopupUI : PopupUI
{
    #region components
    protected NexusUIPresenter _presenter;
    protected NexusStat _nexusStat;
    protected NexusInfoDataSO _nexusInfo;
    #endregion

    public override void Awake()
    {
        base.Awake();
        _presenter = UIManager.Instance.canvasTrm.Find("NexusUI").GetComponent<NexusUIPresenter>();
    }

    protected virtual void Start()
    {
        _nexusStat = NexusManager.Instance.NexusStat;
        _nexusInfo = NexusManager.Instance.NexusInfo;
    }

    public abstract void UIUpdate();
}
