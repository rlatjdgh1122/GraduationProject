using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GroundElements
{
    public Enemy[] Enemies { get; private set; }
    public ResourceObject[] Resources { get; private set; }
    public CostBox Reward { get; private set; }

    public GroundElements(ResourceObject[] resources, CostBox costBox, Enemy[] enemies)
    {
        Resources = resources;
        Reward = costBox;
        Enemies = enemies;
    }
}
