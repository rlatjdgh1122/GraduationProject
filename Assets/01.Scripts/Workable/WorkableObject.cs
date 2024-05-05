using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : TargetObject
{
    public ResourceType resourceType;

    [Header("Noise Setting")]
    [SerializeField] private float _noiseValue;
    public float NoiseValue => _noiseValue;

    [SerializeField] private int _workMotionCount;
    public int WorkMotionCount => _workMotionCount;

    public EntityActionData ActionData { get; private set; }

    protected override void HandleHit()
    {

    }
    protected override void HandleDie()
    {

    }
}
