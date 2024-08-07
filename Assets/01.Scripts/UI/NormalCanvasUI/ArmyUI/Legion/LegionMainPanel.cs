using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionMainPanel : PopupUI
{
    private ModifyLegionPanel _modifyPanel;
    private LegionNamingPanel _namingPanel;

    public override void Awake()
    {
        base.Awake();

        _modifyPanel = GetComponentInChildren<ModifyLegionPanel>();
        _namingPanel = GetComponentInChildren<LegionNamingPanel>();
    }

    private void OnEnable()
    {
        _namingPanel.OnLegionNameNamingEvent += _modifyPanel.ShowLegionName;
    }

    private void OnDisable()
    {
        _namingPanel.OnLegionNameNamingEvent -= _modifyPanel.ShowLegionName;
    }

    public void MoveMainPanel(float x)
    {
        MovePanel(x, 0, _panelFadeTime, true);
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);

        SoundManager.Play2DSound(_soundName);
    }
}
