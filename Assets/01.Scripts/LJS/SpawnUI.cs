using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnUI : MonoBehaviour
{
    [SerializeField] private ClickBuilding _clickBuilding;
    private Transform _uiObj;

    private Sequence seq;

    private void Awake()
    {
        _uiObj = transform.Find("SpawnUI").GetComponent<Transform>();
        seq = DOTween.Sequence();
    }

    public void MoveUI()
    {
        if(_clickBuilding.IsClick)
        {
            _uiObj.transform.DOMoveY(-461.4039f, 1f);
        }
        else
        {
            _uiObj.transform.DOMoveY(-820f, 1f);
        }
    }
}
