using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneFadeIn : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField]
    private float _duration;

    private void Awake()
    {
        _canvasGroup = transform.Find("StartFadeInPanel").GetComponent<CanvasGroup>();

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    private void Start()
    {
        _canvasGroup.DOFade(0, _duration).OnComplete(() => _canvasGroup.blocksRaycasts = false);
    }
}
