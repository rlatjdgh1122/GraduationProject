using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill Skill = null;

    public virtual void OnSkillEvent() { }

    protected override void Awake()
    {
        base.Awake();

        Skill = transform.Find("SkillManager").GetComponent<Skill>();
        Skill?.SetOwner(this);

        
    }

    protected override void Update()
    {
        base.Update();

        OnPlaySkill();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnPlaySkill()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Skill.SkillTransition.CheckDecision())
            {
                OnSkillEvent();

                Skill.SkillTransition.OnUsed();
            }
        }
    }
}
