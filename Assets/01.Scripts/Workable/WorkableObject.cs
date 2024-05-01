using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : TargetObject
{
    public ResourceType resourceType;
    [SerializeField] private float _noiseValue;
    public float NoiseValue => _noiseValue;
    public EntityActionData ActionData { get; private set; }

    protected override void HandleHit()
    {

    }
    protected override void HandleDie()
    {

    }
}
