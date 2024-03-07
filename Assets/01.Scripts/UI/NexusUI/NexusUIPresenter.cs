using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusUIPresenter : PopupUI
{
    #region events
    public Action OnUpdateNexusUI;
    #endregion

    public NexusBase nexusBase;

    public override void Awake()
    {
        base.Awake();
    }

    public void LevelUp()
    {
        nexusBase.NexusStat.maxHealth.AddSum
            (nexusBase.NexusStat.maxHealth.GetValue(), nexusBase.NexusStat.level, nexusBase.NexusStat.levelupIncreaseValue);
        nexusBase.NexusStat.level++;
        nexusBase.NexusStat.upgradePrice *= 2; // <-이건 임시

        OnUpdateNexusUI?.Invoke();
    }   

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
