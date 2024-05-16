using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum StartButtonType
{
    Start,
    Setting,
    Exit,
    Credits
}

public class HoverGround : MonoBehaviour
{
    [SerializeField]
    private StartButtonType _buttonType;

    [SerializeField]
    private float _duration = 1f;

    private float _zPos;

    private Material _material;
    private TextMeshPro _text;
    private Color _noramlColor;

    [SerializeField]
    private Color _changeColor;

    [SerializeField] private UnityEvent OnButtonEvent = null;

    private void Awake()
    {
        _zPos = transform.localPosition.z;

        _material = GetComponent<MeshRenderer>().material;
        _noramlColor = _material.color;
        _text = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    private void OnMouseEnter()
    {
        //_material.EnableKeyword("_EMISSION");

        //_material.SetColor("_EmissionColor", _changeColor);
        _material.SetVector("_EmissionColor", new Vector4(_changeColor.r, _changeColor.g, _changeColor.b, 10));

        _text.DOFade(1, _duration);

        transform.DOLocalMoveZ(-1, _duration);
    }

    private void OnMouseExit()
    {
        //_material.DisableKeyword("_EMISSION");

        //_material.SetColor("_EmissionColor", _noramlColor);
        _material.SetColor("_EmissionColor", Color.black);

        _text.DOFade(0, _duration);
        transform.DOLocalMoveZ(_zPos, _duration);
    }

    private void OnMouseDown()
    {
        OnButtonEvent?.Invoke();
    }
}