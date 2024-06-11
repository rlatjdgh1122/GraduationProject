using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralVerticalPanel : ArmyComponentUI
{
    [Header("Value")]
    [SerializeField] private float _originPos;

    public override void Awake()
    {
        base.Awake();

        OnShowGeneralInfo += ShowInfoLogic;
        OnHideGeneralInfo += HideInfoLogic;
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }

    public void ShowInfoLogic(GeneralStat data)
    {
        ShowPanel();
        MovePanel(574, -245, _panelFadeTime, false);
    }

    public void HideInfoLogic()
    {
        this._panelDelayTime = 0;
        MovePanel(574, _originPos, _panelFadeTime, false);
        generalSlotPanel.ShowPanel();
        this._panelDelayTime = 0.8f;
    }

    protected virtual void OnDestroy()
    {
        OnShowGeneralInfo -= ShowInfoLogic;
        OnHideGeneralInfo -= HideInfoLogic;
    }
}
