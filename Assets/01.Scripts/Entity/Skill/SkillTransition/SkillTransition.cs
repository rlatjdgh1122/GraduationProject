using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTransition : MonoBehaviour
{
    [SerializeField]
    private SKillDecision _decisions;

    public void SetUp(Transform parentRoot)
    {
        _decisions.SetUp(parentRoot);
    }

    public bool CheckDecisions()
    {
        return _decisions.MakeDecision();
    }

    public void Init()
    {
        _decisions.Init();
    }
}
