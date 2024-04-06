using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLegionChange : PopupUI
{
    [SerializeField]
    private float _changeTime;
    [SerializeField]
    private LegionChangeButton _buttonPrefab;

    private Transform _buttonParent;
    protected LegionInventoryManager legion;

    protected List<CanvasGroup> _buttonList = new();

    public override void Awake()
    {
        base.Awake();

        _buttonParent = transform.Find("LegionNumber").GetComponent<Transform>();
        legion = LegionInventoryManager.Instance;

        CreateChangeButton();
    }

    public void CreateChangeButton()
    {
        for (int i = 0; i < legion.LegionCount; i++)
        {
            LegionChangeButton btn = Instantiate(_buttonPrefab);
            btn.transform.parent = _buttonParent;

            btn.CreateBtn(i + 1, legion.LegionList()[i].Price);

            CanvasGroup canvasGroup = btn.GetComponent<CanvasGroup>();
            _buttonList.Add(canvasGroup);
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UIManager.Instance.InitializHudTextSequence();

        for (int i = 0; i < _buttonList.Count; i++)
        {
            UIManager.Instance.HudTextSequence.Append(_buttonList[i].DOFade(1, _changeTime));
        }
    }

    public override void HidePanel()
    {
        base.HidePanel();

        for (int i = _buttonList.Count - 1; i >= 0; i--)
        {
            _buttonList[i].alpha = 0;
        }
    }
}
