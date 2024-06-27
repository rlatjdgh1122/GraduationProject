using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill Skill { get; private set; } = null;
    public Skill Ultimate { get; private set; } = null;

    public virtual void OnSkillEvent() { }
    public virtual void OnUltimateEvent() { }

    protected override void Awake()
    {
        base.Awake();

        Skill = transform.Find("SkillManager").GetComponent<Skill>();
        Ultimate = transform.Find("UltimateManager").GetComponent<Skill>();
    }

    protected override void Start()
    {
        base.Start();

        //호출순서 땜에 일부러 스타트에서 실행
        Skill?.SetOwner(this);
        Ultimate?.SetOwner(this);
    }

    protected override void Update()
    {
        base.Update();

        OnPlaySkill();
        OnPlayUltimateSkill();
    }

    private void OnPlaySkill()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Skill.SkillController.CheckDecision())
            {
                OnSkillEvent();

                Skill.SkillController.OnUsed();
            }

        }//end if
    }

    private void OnPlayUltimateSkill()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Ultimate.SkillController.CheckDecision())
            {
                OnUltimateEvent();

                Ultimate.SkillController.OnUsed();
            }

        }//end if
    }

   
}
