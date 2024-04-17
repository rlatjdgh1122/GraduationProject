using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescriptionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea]
    [SerializeField]
    private string _descript;

    [SerializeField]
    private float _fadeTime;

    public TextMeshProUGUI DescriptText;

    public bool _canShowAll;

    private void Awake()
    {
        DescriptText = transform.Find("DescriptText").GetComponent<TextMeshProUGUI>();

        DescriptText.text = _descript;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptText.DOFade(1, _fadeTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptText.DOFade(0, _fadeTime);
    }

    public void ShowAllDescript(bool value)
    {
        _canShowAll = value;

        if(_canShowAll)
            DescriptText.DOFade(1, _fadeTime);
        else
            DescriptText.DOFade(0, _fadeTime);
    }
}