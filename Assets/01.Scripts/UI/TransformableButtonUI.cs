using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransformableButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float _targetSize;
    [SerializeField] private float _fadeTime;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOScale(_targetSize, _fadeTime).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rectTransform.DOScale(1, _fadeTime).SetUpdate(true);
    }
}
