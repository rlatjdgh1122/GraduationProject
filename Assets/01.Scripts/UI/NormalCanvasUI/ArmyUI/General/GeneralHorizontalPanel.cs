using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralHorizontalPanel : ArmyComponentUI
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
        MovePanel(0, -15 , _panelFadeTime);
    }

    public void HideInfoLogic()
    {
        this._panelDelayTime = 0.5f;
        MovePanel(_originPos, -15, _panelFadeTime);
        this._panelDelayTime = 0f;
    }

    protected virtual void OnDestroy()
    {
        OnShowGeneralInfo -= ShowInfoLogic;
        OnHideGeneralInfo -= HideInfoLogic;
    }
}
