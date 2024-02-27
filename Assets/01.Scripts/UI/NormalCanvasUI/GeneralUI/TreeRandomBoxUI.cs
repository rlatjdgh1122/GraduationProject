using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum RandomTreeType
{
    General,
    Legion
}

[Serializable]
public struct RandomEffect
{
    public int id;
    public string description;
}

public class TreeRandomBoxUI : MonoBehaviour
{
    public RandomTreeType RandomType;
    public List<RandomEffect> _effects;

    private RandomEffect _selectedEffect;

    private TreeUI _treeUI;
    [HideInInspector] public CanvasGroup canvasGroup;
    private RectTransform _trm;

    private TextMeshProUGUI _categoryText;
    private TextMeshProUGUI _buffText;

    private void Awake()
    {
        _treeUI = transform.parent.parent.GetComponent<TreeUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        _trm = GetComponent<RectTransform>();
        _categoryText = transform.Find("category").GetComponent<TextMeshProUGUI>();
        _buffText = transform.Find("buffText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _treeUI.TreeBoxList.Add(this);
    }

    public void SetRandom()
    {
        if (_effects.Count == 0) return;

        if (RandomType == RandomTreeType.General)
        {
            int randomIndex = Random.Range(0, _effects.Count);
            _selectedEffect = _effects[randomIndex];

            _categoryText.text = "¿Â±∫";
            _buffText.text = _selectedEffect.description;
        }

    }

    public void Select()
    {
        _treeUI.TreeBoxList.Remove(this);
        _treeUI.CloseUnselectedBox();
        _trm.DOAnchorPosX(96, 0.6f);
    }
}
