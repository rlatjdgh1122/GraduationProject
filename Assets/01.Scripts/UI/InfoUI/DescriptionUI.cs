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

    private TextMeshProUGUI _descriptText;

    private void Awake()
    {
        _descriptText = transform.Find("DescriptText").GetComponent<TextMeshProUGUI>();

        _descriptText.text = _descript;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptText.DOFade(1, _fadeTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptText.DOFade(0, _fadeTime);
    }
}