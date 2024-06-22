using System;
using UnityEngine;

public class General : Penguin, ISkillable
{
    public Skill Skill = null;

    private SkillTransition _skillTransition = null;
    public virtual void OnSkillEvent() { }

    protected override void Awake()
    {
        base.Awake();

        Skill = transform.Find("SkillManager").GetComponent<Skill>();
        Skill?.SetOwner(this);

        _skillTransition = transform.Find("SkillTransition").GetComponent<SkillTransition>();
        _skillTransition.SetUp(transform);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (_skillTransition.CheckDecisions())
            {
                OnSkillEvent();

                _skillTransition.Init();
            }
        }
    }

    protected override void Start()
    {
        base.Start();
    }
}
