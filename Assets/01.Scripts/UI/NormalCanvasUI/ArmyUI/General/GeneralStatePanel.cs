using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralStatePanel : ArmyComponentUI
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _generalCountText;
    [SerializeField] private GameObject _purchaseButton;
    [SerializeField] private GameObject _completeBox;

    private int _maxGeneralCount;

    public override void Awake()
    {
        base.Awake();
        _maxGeneralCount = GeneralManager.Instance.GeneralList.Count;
        OnUpdateState += SetUI;
    }

    private void OnDisable()
    {
        OnUpdateState -= SetUI;
    }

    private void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        _generalCountText.text = $"{GeneralManager.Instance.OwnGeneral.Count}/{_maxGeneralCount}";

        if (GeneralManager.Instance.GeneralList.Count <= 0)
        {
            _purchaseButton.SetActive(false);
            _completeBox.SetActive(true);
        }
    }
}
