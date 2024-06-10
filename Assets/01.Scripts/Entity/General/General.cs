using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill skill;

    public bool canSpinAttack = false;

    public Action<Penguin> OnSynergyEvent = null;

    public virtual void OnSkillEvent() { }

    protected override void Awake()
    {
        base.Awake();

        skill = transform.Find("SkillManager").GetComponent<Skill>();
        skill?.SetOwner(this);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.K))
        {
            OnSkillEvent();
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
