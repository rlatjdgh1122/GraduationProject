using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill Skill = null;

    public virtual void OnSkillEvent() { }
    public virtual void OnUltimateEvent() { }

    protected override void Awake()
    {
        base.Awake();

        Skill = transform.Find("SkillManager").GetComponent<Skill>();
    }

    protected override void Start()
    {
        base.Start();
        
        //호출순서 땜에 일부러 스타트에서 실행
        Skill?.SetOwner(this);
    }

    protected override void Update()
    {
        base.Update();

        OnPlaySkill();
    }

    private void OnPlaySkill()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Skill.SkillTransition.CheckDecision())
            {
                OnSkillEvent();

                Skill.SkillTransition.OnUsed();
            }
        }

        if(Input.GetKeyDown(KeyCode.G))
        {
            if (Skill.SkillTransition.CheckDecision())
            {
                OnUltimateEvent();

                Skill.SkillTransition.OnUsed();
            }
        }
    }
}
