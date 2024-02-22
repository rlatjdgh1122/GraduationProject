using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum TweenType
{
    Move,
    Fade,
    Scale,
    All
}

public enum TweenDirection
{
    X,
    Y,
    All
}

[Serializable]
public class CustomButtonEvent
{
    public bool TweenBtn = false;

    public bool OnGameObjectTween = false;
    public bool OffGameObjectTween = false;

    [HideInInspector] public TweenType Type;
    [HideInInspector] public TweenDirection Direction;

    [HideInInspector] public float MoveX;
    [HideInInspector] public float MoveY;
    [HideInInspector] public Vector3 MoveAll;

    [HideInInspector] public float ScaleX;
    [HideInInspector] public float ScaleY;
    [HideInInspector] public Vector3 ScaleAll;

    [HideInInspector] public float Time;
    [HideInInspector] public float FadeValue;
}

[RequireComponent(typeof(Button))]
public class ButtonEvent : MonoBehaviour
{    
    [SerializeField] private List<Transform> OffObjectList;
    [SerializeField] private List<Transform> OnObjectList;

    public CustomButtonEvent InspectorCustomBtn;

    private RectTransform _rect;

    private Vector3 _transform;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();

        _transform = _rect.localPosition;
    }

    public void OnOffObject()
    {
        foreach(var obj in OffObjectList)
        {
            if (InspectorCustomBtn.OffGameObjectTween)
            {
                TweenBranch(obj);
            }

            obj.gameObject.SetActive(false);
        }
        foreach (var obj in OnObjectList)
        {
            if(InspectorCustomBtn.OnGameObjectTween)
            {
                TweenBranch(obj);
            }

            obj.gameObject.SetActive(true);
        }
    }

    private void TweenBranch(Transform trm)
    {
        switch (InspectorCustomBtn.Type)
        {
            case TweenType.Move:
                TweenTypeMove(trm);
                break;
            case TweenType.Scale:
                TweenTypeScale(trm);
                break;
            case TweenType.Fade:
                TweenTypeFade(trm);
                break;
            default:
                TweenTypeMove(trm);
                TweenTypeScale(trm);
                TweenTypeFade(trm);
                break;
        }
    }

    private void TweenTypeMove(Transform trm)
    {
        switch(InspectorCustomBtn.Direction)
        {
            case TweenDirection.X:
                trm.DOLocalMoveX(_transform.x + InspectorCustomBtn.MoveX, InspectorCustomBtn.Time);
                break;
            case TweenDirection.Y:
                trm.DOLocalMoveY(_transform.y + InspectorCustomBtn.MoveY, InspectorCustomBtn.Time);
                break;
            default:
                trm.DOLocalMove(_transform + InspectorCustomBtn.MoveAll, InspectorCustomBtn.Time);
                break;
        }
    }

    private void TweenTypeScale(Transform trm)
    {
        switch (InspectorCustomBtn.Direction)
        {
            case TweenDirection.X:
                trm.DOScaleX(InspectorCustomBtn.ScaleX, InspectorCustomBtn.Time);
                break;
            case TweenDirection.Y:
                trm.DOScaleY(InspectorCustomBtn.ScaleY, InspectorCustomBtn.Time);
                break;
            default:
                trm.DOScale(InspectorCustomBtn.ScaleAll, InspectorCustomBtn.Time);
                break;
        }
    }

    private void TweenTypeFade(Transform trm)
    {
        CanvasGroup canvasGroup = trm.GetComponent<CanvasGroup>();

        if(InspectorCustomBtn.FadeValue <= 0)
        {
            canvasGroup.blocksRaycasts = false;
        }

        canvasGroup.DOFade(InspectorCustomBtn.FadeValue, InspectorCustomBtn.Time);
    }
}