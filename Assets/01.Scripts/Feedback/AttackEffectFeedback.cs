using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffectFeedback : Feedback
{
    [SerializeField] private GameObject effectObj;

    protected override void Start()
    {
        effectObj?.SetActive(false);
    }
    public override void CreateFeedback()
    {
        effectObj?.SetActive(true);
    }

    public override void FinishFeedback()
    {
        effectObj?.SetActive(false);
    }
}
