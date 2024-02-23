using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Image _ui;

    [SerializeField] private PenguinStat _itemData;

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (_itemData == null) return;
        if (_ui == null)
        {
            _ui = GetComponent<Image>();
        }
        _ui.sprite = _itemData.PenguinIcon;
        gameObject.name = $"ItemObject[{_itemData.PenguinName}]";
    }

#endif

    private void Awake()
    {
        _ui = GetComponent<Image>();
    }
}
