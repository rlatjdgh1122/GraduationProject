using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionModeChange : MonoBehaviour
{
    [SerializeField]
    private float _duration = 1f;

    [SerializeField]
    private float _screenPos = 2170;
    private RectTransform _gameOptionTrm;
    private RectTransform _operationKeyTrm;

    private void Awake()
    {
        _gameOptionTrm = transform.Find("GameOptionPanel").GetComponent<RectTransform>();
        _operationKeyTrm = transform.Find("OperationKeyPanel").GetComponent<RectTransform>();
    }

    public void GameOptionButtonClick()
    {
        _gameOptionTrm.DOLocalMoveX(0, _duration).SetUpdate(true);
        _operationKeyTrm.DOLocalMoveX(_screenPos, _duration).SetUpdate(true);
    }

    public void OperationKeyButtonClick()
    {
        _gameOptionTrm.DOLocalMoveX(-_screenPos, _duration).SetUpdate(true);
        _operationKeyTrm.DOLocalMoveX(0, _duration).SetUpdate(true);
    }
}