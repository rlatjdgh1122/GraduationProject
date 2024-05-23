using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private TargetObject _target;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private RectTransform _text;

    [SerializeField] private GameObject[] _group;

    private int _index = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _entity.CurrentTarget = _target;
            _canvas.gameObject.SetActive(false);
            _text.DOAnchorPosX(0, 1.5f).SetEase(Ease.OutQuint);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _group[_index].gameObject.SetActive(false);
            _group[_index++].gameObject.SetActive(true);
        }
    }
}
