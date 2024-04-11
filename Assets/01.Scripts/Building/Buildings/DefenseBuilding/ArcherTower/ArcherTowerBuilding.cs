using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTowerBuilding : DefenseBuilding
{
    ArcherTowerPenguin[] _archerPenguins;

    protected override void Awake()
    {
        base.Awake();

        _archerPenguins = transform.GetComponentsInChildren<ArcherTowerPenguin>();
        SetUpPenguinsCompo(FOV.ViewRadius, true);

    }

    private void SetUpPenguinsCompo(float attackDistance, bool isFirst = false)
    {
        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].attackDistance = attackDistance;

            if (isFirst)
            {
                _archerPenguins[i].gameObject.SetActive(false);
            }
        }
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].gameObject.SetActive(true);
        }
    }

    protected override void Running()
    {

    }
}
