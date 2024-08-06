using System;
using UnityEngine;
using UnityEngine.Events;

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

    //쉴드 장군 때문에 만들었음 ㅋㅋ
    public UnityEvent OnAvaliableEvent = null;
    public UnityEvent OnUnavailableEvent = null;
    #endregion

    public bool CanUseSkill = false;

    public bool IsAvaliable
    {
        get => _isAvaliable;
        set
        {
            _isAvaliable = value;
            if (value == true)
            {
                OnAvaliableEvent?.Invoke();
            }
            else
            {
                OnUnavailableEvent?.Invoke();
            }
        }
    }

    private bool _isAvaliable = true;

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
