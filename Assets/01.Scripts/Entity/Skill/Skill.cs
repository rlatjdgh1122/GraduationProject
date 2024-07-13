using System;
using UnityEngine;

[RequireComponent(typeof(SkillActionData))]
public abstract class Skill : MonoBehaviour
{
    protected Entity _owner;

    public SkillController SkillController { get; private set; } = null;
    protected EntityActionData _entityActionData { get; private set; } = null;
    protected SkillActionData _skillActionData { get; private set; } = null;

    #region events
    public Action OnSkillStart = null;
    public Action OnSkillCompleted = null;
    #endregion

    public bool IsAvaliable = true;
    public bool CanUseSkill = false;

    protected int maxDecisionValue => SkillController.GetMaxValue;
    private void Awake()
    {
        SkillController = transform.Find("SkillTransition").GetComponent<SkillController>();
    }

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;

        _entityActionData = owner.GetComponent<EntityActionData>();
        _skillActionData = transform.GetComponent<SkillActionData>();
        SkillController.SetUp(owner.transform);
    }

    public virtual void PlaySkill()
    {

    }
}
