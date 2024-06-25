using UnityEngine;

public abstract class SKillDecision : MonoBehaviour
{
    protected EntityActionData _actionData;

    public virtual void SetUp(Transform parentRoot)
    {
        _actionData = parentRoot.GetComponent<EntityActionData>();
    }

    public abstract bool MakeDecision();

    public abstract void ResetValue();

    public abstract float GetDecisionValue();
}

