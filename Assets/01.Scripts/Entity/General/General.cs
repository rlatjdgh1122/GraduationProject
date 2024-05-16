using System;

public class General : Penguin
{
    public Skill skill;

    public bool canSpinAttack = false;

    public Action<Penguin> OnSynergyEvent = null;

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
