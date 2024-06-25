using System;
using UnityEngine;

public abstract class SKillDecision : MonoBehaviour
{
    protected EntityActionData _actionData;

    public Action OnSkillUsedEvent = null;
    public Action OnSkillReadyEvent = null;

    public virtual void SetUp(Transform parentRoot)
    {
        _actionData = parentRoot.GetComponent<EntityActionData>();
    }


    public abstract bool MakeDecision();

    public abstract void OnUsed(); //스킬 사용한 후 실행됨

    public abstract void LevelUp(); //레벨업하면 어케해줄 것인가

}

