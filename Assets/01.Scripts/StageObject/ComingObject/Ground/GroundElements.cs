using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundElements : ComingElements
{
    public ResourceObject[] Resources { get; private set; }
    public CostBox Reward { get; private set; }

    public GroundElements(List<Enemy> enemies, ResourceObject[] resources, CostBox costBox) : base(enemies)
    {
        Resources = resources;
        Reward = costBox;

        if (Reward != null)
        {
            Elements.Add(costBox.transform);
        }

        foreach(ResourceObject element in Resources)
        {
            Elements.Add(element.transform);
        }
    }
}
