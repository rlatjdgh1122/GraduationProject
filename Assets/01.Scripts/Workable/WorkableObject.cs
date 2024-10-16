using System;
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

    [SerializeField] private float _maxNoiseValue;
    public float MaxNoiseValue => _maxNoiseValue;

    private float _currentNoiseValue;
    public float CurrentNoiseValue => _currentNoiseValue;

    public EntityActionData ActionData { get; private set; }

    public Action OnNoiseExcessEvent = null;

    protected override void Awake()
    {
        base.Awake();

        ActionData = GetComponent<EntityActionData>();
    }

    protected override void HandleHit()
    {

    }

    public void ResetNoise()
    {
        _currentNoiseValue = 0;
    }

    public void IncreaseCurrentNoise()
    {
        _currentNoiseValue += _noiseValue;

        NoiseManager.Instance.AddViewNoise(_noiseValue);

        if (_currentNoiseValue > _maxNoiseValue)
        {
            OnNoiseExcessEvent?.Invoke();
        }
    }
}
