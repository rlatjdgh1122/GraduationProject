using Bitgem.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class General : Penguin
{
    public Skill skill;

    public bool canSpinAttack = false;

    protected override void Awake()
    {
        base.Awake();

        skill = transform.Find("SkillManager").GetComponent<Skill>();
        skill?.SetOwner(this);
    }

    protected override void Start()
    {
        base.Start();
    }
}
