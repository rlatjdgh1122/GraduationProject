using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TutorialImage
{
    None = 0,
    Warning,
    Arrow_Right,
    Arrow_Left,
    Arrow
}

[Serializable]
public class TutorialSelectImage
{
    public TutorialImage Type;
    public GameObject SelectObj;
}

public class TutorialWorldCanvas : WorldUI
{
    public List<TutorialSelectImage> _selectImage = new();
    private Transform _target;
    private float _yPos;

    public void Init(TutorialImage type)
    {
        foreach(var image in _selectImage)
        {
            if(image.Type == type)
            {
                image.SelectObj.SetActive(true);
            }
            else
            {
                image.SelectObj.SetActive(false);
            }
        }
    }

    public void SetTarget(Transform trm, float y)
    {
        _target = trm;
        _yPos = y;
    }

    public override void Update()
    {
        base.Update();

        if(_target != null)
        {
            Vector3 targetPos = _target.position;
            targetPos.y = _yPos;

            transform.position = targetPos;
        }
    }
}