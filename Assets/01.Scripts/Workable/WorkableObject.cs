using UnityEngine;

public enum ResourceType
{
    Stone,
    Wood,
}

public class WorkableObject : Target
{
    public ResourceType resourceType;

    [SerializeField] protected BaseStat _characterStat;
    [SerializeField] private float _noiseValue;
    public float NoiseValue => _noiseValue;

    public Health HealthCompo { get; private set; }
    public EntityActionData ActionData { get; private set; }

    protected override void HandleHit()
    {
        
    }
    protected override void HandleDie()
    {
        
    }

}
