using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : Singleton<GeneralManager>
{
    [SerializeField] private List<GeneralStat> _generalList;

    public List<GeneralStat> GeneralList => _generalList;
    public List<GeneralStat> OwnGeneral = null;

    public override void Awake()
    {
        for (int i = 0; i < _generalList.Count; i++)
        {
            _generalList[i] = Instantiate(_generalList[i]);
        }
    }
}
