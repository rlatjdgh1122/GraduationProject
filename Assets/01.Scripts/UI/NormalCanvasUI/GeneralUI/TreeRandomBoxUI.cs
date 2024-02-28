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

            _categoryText.text = "장군";
            _buffText.text = $"장군의 {_selectedEffect.statType} {_selectedEffect.increasePercentage}% 증가";

            //_randomEffectSO.generalData.Remove(_selectedEffect); //선택된건 이제 영구적으로 삭제
        }
        else if (RandomType == RandomTreeType.Legion)
        {
            int randomIndex = Random.Range(0, _randomEffectSO.legionData.Count);

            _selectedEffect = _randomEffectSO.legionData[randomIndex];

            _categoryText.text = "군단";
            _buffText.text = $"장군이 포함된 군단의 {_selectedEffect.statType} {_selectedEffect.increasePercentage}% 증가";

            //_randomEffectSO.legionData.Remove(_selectedEffect); //선택된건 이제 영구적으로 삭제
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
