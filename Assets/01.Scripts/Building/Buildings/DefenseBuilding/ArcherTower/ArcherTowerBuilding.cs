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
        SetUpPenguinsCompo(FOV.ViewRadius);

    }

    private void SetUpPenguinsCompo(float attackDistance)
    {
        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].attackDistance = attackDistance;
        }
    }

    protected override void SetInstalled()
    {
        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].gameObject.SetActive(true);
            _archerPenguins[i].SetInstalled();
        }


        base.SetInstalled();
    }

    protected override void Running()
    {

    }
}
