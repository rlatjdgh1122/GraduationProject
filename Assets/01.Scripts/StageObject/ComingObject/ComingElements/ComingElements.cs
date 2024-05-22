using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingElements
{
    public List<Transform> Elements { get; private set; }

    public List<Enemy> Enemies { get; private set; }

    public ComingElements(List<Enemy> enemies)
    {
        Elements = new List<Transform>();

        Enemies = enemies;

        foreach (Enemy enemy in Enemies)
        {
            Elements.Add(enemy.transform);
        }
    }
}
