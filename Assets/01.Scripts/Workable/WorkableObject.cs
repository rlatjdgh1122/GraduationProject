using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : Target
{
    public ResourceType resourceType;
    public EntityActionData ActionData { get; private set; }

    protected override void HandleHit()
    {
        
    }
    protected override void HandleDie()
    {
        
    }

}
