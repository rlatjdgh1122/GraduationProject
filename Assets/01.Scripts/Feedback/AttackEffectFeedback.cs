using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectFeedback : Feedback
{
    [SerializeField] private GameObject effectObj;

    protected override void Start()
    {
        effectObj.gameObject.SetActive(false);
    }
    public override void CreateFeedback()
    {
        if(!effectObj.activeInHierarchy)
        {
            effectObj.gameObject.SetActive(true);
        }
    }

    public override void FinishFeedback()
    {
        effectObj.gameObject.SetActive(false);
    }
}
