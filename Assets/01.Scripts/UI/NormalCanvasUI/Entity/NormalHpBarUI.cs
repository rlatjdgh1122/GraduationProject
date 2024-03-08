using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalHpBarUI : MonoBehaviour
{
    [SerializeField] private float _waitToDisapper;
    private CanvasGroup _container;
    private Image _hpbar;

    private void Awake()
    {
        _hpbar = GetComponent<Image>(); 
        _container = transform.parent.parent.GetComponent<CanvasGroup>();
    }

    public void UpdateHpbarUI(float current, float max)
    {
        _hpbar.DOFillAmount(current / max, _waitToDisapper);
    }

    public void OffHpbarsUI()
    {
        _container.DOFade(0, _waitToDisapper);
    }
}
