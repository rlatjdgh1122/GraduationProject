using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLegionChange : PopupUI, ICreateSlotUI
{
    [SerializeField]
    private float _changeTime;
    [SerializeField]
    private LegionChangeButton _buttonPrefab;

    private Transform _buttonParent;
    protected LegionInventoryManager legion;

    protected List<LegionChangeButton> _buttonList = new();

    public override void Awake()
    {
        base.Awake();

        _buttonParent = transform.Find("LegionNumber").GetComponent<Transform>();
        legion = LegionInventoryManager.Instance;

        CreateSlot();
    }

    public void CreateSlot()
    {
        for (int i = 0; i < legion.LegionCount; i++)
        {
            LegionChangeButton btn = Instantiate(_buttonPrefab);
            btn.transform.SetParent(_buttonParent);

            btn.CreateBtn(i + 1, legion.LegionList[i].Price);
            if (!LegionInventoryManager.Instance.LegionList[i].Locked)
                btn.UnLocked();

            _buttonList.Add(btn);
        }
    }

    public void ShowChangeLegionPanel()
    {
        if(WaveManager.Instance.CurrentWaveCount < 5)
        {
            UIManager.Instance.ShowWarningUI("튜토리얼이 진행 중입니다");
            return;
        }

        ShowPanel();

        UIManager.Instance.InitializHudTextSequence();

        for (int i = 0; i < _buttonList.Count; i++)
        {
            UIManager.Instance.HudTextSequence.Append(_buttonList[i].canvasGroup.DOFade(1, _changeTime));
        }
    }

    public void HideChangeLegionPanel()
    {
        HidePanel();

        for (int i = _buttonList.Count - 1; i >= 0; i--)
        {
            _buttonList[i].canvasGroup.alpha = 0;
        }
    }
}
