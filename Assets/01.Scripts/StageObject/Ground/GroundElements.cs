using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GroundElements
{
    public Enemy[] Enemies { get; private set; }
    public ResourceObject[] Resources { get; private set; }
    public CostBox Reward { get; private set; }

    public List<Transform> Elements { get; private set; }

    public GroundElements(ResourceObject[] resources, CostBox costBox, Enemy[] enemies)
    {
        Elements = new List<Transform>();

        Resources = resources;
        Reward = costBox;
        Enemies = enemies;

        if (Reward != null)
        {
            Elements.Add(costBox.transform);
        }

        foreach (Enemy enemy in Enemies)
        {
            Elements.Add(enemy.transform);
        }

        foreach(ResourceObject element in Resources)
        {
            Elements.Add(element.transform);
        }



    }
}
