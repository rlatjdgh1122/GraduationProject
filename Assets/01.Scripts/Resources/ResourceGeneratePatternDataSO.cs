using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ResourceGeneratePattern")]
public class ResourceGeneratePatternDataSO : ScriptableObject
{
    public List<ResourceGeneratePattern> ResourceGeneratePatterns = new List<ResourceGeneratePattern>();
}
