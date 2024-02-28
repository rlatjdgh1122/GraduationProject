using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TreeRandomBoxUI : MonoBehaviour
{
    public RandomTreeType RandomType;
    [SerializeField] private RandomEffectSO _randomEffectSO;
    private Effects _selectedEffect;

    private TreeUI _treeUI;
    [HideInInspector] public CanvasGroup canvasGroup;
    [HideInInspector] public Button button;
    private RectTransform _trm;
    private TextMeshProUGUI _categoryText;
    public TextMeshProUGUI CategoryText => _categoryText;
    private TextMeshProUGUI _buffText;

    private void Awake()
    {
        _treeUI = transform.parent.parent.GetComponent<TreeUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        _trm = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        _categoryText = transform.Find("category").GetComponent<TextMeshProUGUI>();
        _buffText = transform.Find("buffText").GetComponent<TextMeshProUGUI>();
    }

    public void SetRandom()
    {
        if (RandomType == RandomTreeType.General)
        {
            int randomIndex = Random.Range(0, _randomEffectSO.generalData.Count);

            _selectedEffect = _randomEffectSO.generalData[randomIndex];

            _categoryText.text = "�屺";
            _buffText.text = $"�屺�� {_selectedEffect.statType} {_selectedEffect.increasePercentage}% ����";

            //_randomEffectSO.generalData.Remove(_selectedEffect); //���õȰ� ���� ���������� ����
        }
        else if (RandomType == RandomTreeType.Legion)
        {
            int randomIndex = Random.Range(0, _randomEffectSO.legionData.Count);

            _selectedEffect = _randomEffectSO.legionData[randomIndex];

            _categoryText.text = "����";
            _buffText.text = $"�屺�� ���Ե� ������ {_selectedEffect.statType} {_selectedEffect.increasePercentage}% ����";

            //_randomEffectSO.legionData.Remove(_selectedEffect); //���õȰ� ���� ���������� ����
        }
    }

    public void Select()
    {
        _treeUI.GeneralStat.AddStat(_selectedEffect.increasePercentage, _selectedEffect.statType, _selectedEffect.statMode);

        button.enabled = false;
        _treeUI.TreeBoxList.Remove(this);
        _treeUI.CloseUnselectedBox();
        _trm.DOAnchorPosX(96, 0.6f);
    }
}
