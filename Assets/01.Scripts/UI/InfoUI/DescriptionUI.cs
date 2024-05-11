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

    private void Awake()
    {
        DescriptText = transform.Find("DescriptText").GetComponent<TextMeshProUGUI>();

        DescriptText.text = _descript;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptText.alpha = 1;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptText.alpha = 0;
    }
}