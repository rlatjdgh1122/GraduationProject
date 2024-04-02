using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusPopupUI : PopupUI
{
    #region components
    protected NexusUIPresenter _presenter;
    protected NexusStat _nexusStat;
    protected BuildingDatabaseSO _buildingDatabase;
    #endregion

    protected event Action OnUIUpdate;

    public override void Awake()
    {
        base.Awake();

        _presenter = UIManager.Instance.canvasTrm.Find("NexusUI").GetComponent<NexusUIPresenter>();
    }

    protected virtual void Start()
    {
        _nexusStat = NexusManager.Instance.NexusStat;
        _buildingDatabase = NexusManager.Instance.BuildingDatabase;
    }

    public virtual void OnClick()
    {
        OnUIUpdate?.Invoke();
    }
}
