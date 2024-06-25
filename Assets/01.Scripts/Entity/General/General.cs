using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill Skill = null;
    public SkillTransition SkillTransition = null;

    public virtual void OnSkillEvent() { }

    protected override void Awake()
    {
        base.Awake();

        Skill = transform.Find("SkillManager").GetComponent<Skill>();
        Skill?.SetOwner(this);

        SkillTransition = transform.Find("SkillTransition").GetComponent<SkillTransition>();
        SkillTransition.SetUp(transform);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (SkillTransition.CheckDecision())
            {
                OnSkillEvent();

                SkillTransition.Reset();
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
