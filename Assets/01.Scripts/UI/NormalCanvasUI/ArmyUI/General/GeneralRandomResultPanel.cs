using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralRandomResultPanel : ArmyComponentUI
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _infoButton;

    private List<GeneralStat> _generalList => GeneralManager.Instance.GeneralList;
    private GeneralStat _currentGeneral;

    private List<GeneralStat> _ownGeneralList => GeneralManager.Instance.OwnGeneral;

    public override void Awake()
    {
        base.Awake();
    }

    public override void ShowPanel()
    {
        SetRandom();
        base.ShowPanel();
        SetUI();
    }

    private void SetRandom()
    {
        if (_generalList.Count <= 0) return;

        int randomIndex = Random.Range(0, _generalList.Count);
        _currentGeneral = _generalList[randomIndex];
        _currentGeneral.GeneralDetailData.IsAvailable = true;

        _ownGeneralList.Add(_currentGeneral);
        _generalList.Remove(_currentGeneral);

        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(_currentGeneral.InfoData);
        PenguinManager.Instance.InsertGeneralDummyPenguin(dummy);

        _infoButton.onClick.AddListener(() => ShowGeneralInfo(_currentGeneral));
    }

    private void SetUI()
    {
        _nameText.text = _currentGeneral.InfoData.PenguinName;
        _icon.sprite = _currentGeneral.InfoData.PenguinIcon;
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}